using HWL.CollectCore.Config;
using HWL.CollectCore.Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.CollectCore
{
    public class Collection
    {
        public List<RuleConfigModel> Configs { get; set; }
        public Func<ICollectListener> CollectListener { get; set; }
        public bool IsContinue { get; set; } = true;

        public Collection() { }

        public Collection(string configPath, Func<ICollectListener> collectListener = null) : this(RuleConfigBuilder.GetConfigs(configPath), collectListener) { }

        public Collection(List<RuleConfigModel> configs, Func<ICollectListener> collectListener = null)
        {
            this.Configs = configs;
            this.CollectListener = collectListener;

            if (this.CollectListener == null)
                this.CollectListener = () => new DefaultCollectListener();
        }

        public void Execute(RuleConfigModel config)
        {
            if (config == null)
                throw new ArgumentNullException("RuleConfig");

            if (!config.IsValid())
                throw new Exception("Rule config content is invalid.");

            ICollectListener collectListener = this.CollectListener();
            collectListener.OnStart(config.Description, config.Url, config.Level);

            RuleParser ruleParser = new RuleParser(config.Url, config.Level, config.GetEncoding(), config.Rules);
            CollectContinueModel continueModel = this.IsContinue ? collectListener.OnContinue(config.Description, config.Url) : null;
            if (continueModel != null)
            {
                ruleParser.LevelProcess(continueModel.ContinueUrl, continueModel.ContinueLevel, collectListener);
                this.ContinueExecute(ruleParser, config, collectListener);
            }
            else
            {
                ruleParser.LevelProcess(collectListener);
            }

            collectListener.OnEnd(config.Description, config.Url, config.Level);
        }

        private void ContinueExecute(RuleParser ruleParser, RuleConfigModel config, ICollectListener collectListener)
        {
            CollectContinueModel continueModel = collectListener.OnContinue(config.Description, config.Url);
            if (continueModel != null)
            {
                ruleParser.LevelProcess(continueModel.ContinueUrl, continueModel.ContinueLevel, collectListener);
                this.ContinueExecute(ruleParser, config, collectListener);
            }
        }

        public void Execute()
        {
            if (this.Configs == null || this.Configs.Count <= 0)
                throw new ArgumentNullException("RuleConfigs");

            var invalidConfig = this.Configs.FirstOrDefault(c => !c.IsValid());
            if (invalidConfig != null)
                throw new Exception($"Rule config of {invalidConfig.Description} is invalid.");

            List<RuleConfigModel> enableConfigs = this.Configs.Where(c => c.Enable).ToList();
            Parallel.ForEach(enableConfigs, c =>
            {
                if (c != null)
                    this.Execute(c);
            });
        }
    }
}
