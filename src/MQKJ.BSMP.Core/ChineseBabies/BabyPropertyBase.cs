using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 宝宝属性表
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BabyPropertyBase<TKey> : FullAuditedEntity<TKey>
    {
        /// <summary>
        /// 智力
        /// </summary>
        private int _intelligence { get; set; }
        public virtual int Intelligence
        {
            get => _intelligence < 0 ? 0 : _intelligence;
            set => _intelligence = value < 0 ? 0 : value;
        }

        /// <summary>
        /// 体魄
        /// </summary>
        private int _physique { get; set; }
        public virtual int Physique
        {
            get => _physique < 0 ? 0 : _physique;
            set => _physique = value < 0 ? 0 : value;
        }
        /// <summary>
        /// 想象
        /// </summary>
        private int _imagine { get; set; }
        public virtual int Imagine
        {
            get => _imagine < 0 ? 0 : _imagine;
            set => _imagine = value < 0 ? 0 : value;
        }
        /// <summary>
        /// 意志
        /// </summary>
        private int _willPower { get; set; }
        public virtual int WillPower
        {
            get => _willPower < 0 ? 0 : _willPower;
            set => _willPower = value < 0 ? 0 : value;
        }
        /// <summary>
        /// 情商
        /// </summary>
        private int _emotionQuotient { get; set; }
        public virtual int EmotionQuotient
        {
            get => _emotionQuotient < 0 ? 0 : _emotionQuotient;
            set => _emotionQuotient = value < 0 ? 0 : value;
        }
        /// <summary>
        /// 魅力
        /// </summary>
        private int _charm { get; set; }
        public virtual int Charm
        {
            get => _charm < 0 ? 0 : _charm;
            set => _charm = value < 0 ? 0 : value;
        }
        /// <summary>
        /// 健康
        /// </summary>

        private int _healthy { get; set; }
        public virtual int Healthy
        {
            //get => _healthy > 100 ? 100 : _healthy;
            get
            {
                if (_healthy < 0)
                {
                    return 0;
                }
                else if (_healthy > 100)
                {
                    return 100;
                }else
                {
                    return _healthy;
                }
            }
            set
            {
                _healthy = value > 100 ? 100 : value;
                _healthy = value < 0 ? 0 : value;
            }
            //set => _healthy = value > 100 ? 100 : value;
        }
        /// <summary>
        /// 精力
        /// </summary>
        private int _energy { get; set; }

        public virtual int Energy
        {
            //get => _energy > 100 ? 100 : _energy;
            //set => _energy = value > 100 ? 100 : value;
            get
            {
                if (_energy < 0)
                {
                    return 0;
                }
                else if (_energy > 100)
                {
                    return 100;
                }
                else
                {
                    return _energy;
                }
            }
            set
            {
                _energy = value > 100 ? 100 : value;
                _energy = value < 0 ? 0 : value;
            }
        }
        [NotMapped]
        public int PropertyTotal
        {
            get => Charm + WillPower + Imagine + EmotionQuotient + Physique + Intelligence;
        }
    }
}
