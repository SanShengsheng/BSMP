using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public class TextAudioCreator
    {

        private readonly BSMPDbContext _context;
        private readonly string[] flagTextAudios = new string[] {
"hi,homie"
, "我是中华锦鲤，欧皇附体!"
, "好了没啊，快点啦~"
, "约定引起极度舒适~"
, "可以申请换一个吗?"
, "我现在慌的一批…"
, "不搞你一下，都对不起祖国的花朵~"
,"网络又走神了…"
,"skr skr skr"
,"我感觉这把要凉了…"
,"按住暴躁的你~"
,"皮这一下，很开心？"
,"我期待的画面出现了~"
,"我觉得可以，我觉得OK。"
,"你的良心不会痛吗？"
            };
        private readonly string[] answerTextAudios = new string[] {
            "可把我牛逼坏了!"
,"皇上，思虑周详了吗？"
,"三个都不符合我的气质…"
,"都想选，都是我的菜!!"
,"这些选项什么鬼?!"
,"篇幅已超出我的视线…"
,"故事和我一见钟情!!"
,"哈哈，好沙雕的故事。"
,"难skr人，你到底会怎么选呢？"
,"网络又走神了…"
,"你这么厉害，你咋不上天呢?"
,"容朕想想…"
,"选一个最有可能的！！"
,"挑个最爱吃的？"
,"你真是个小机灵鬼！"
,"隐形眼镜，了解一下？"
,"请说出你的故事…"
,"不能同意更多…"
,"哈哈，不告诉你，你猜嘛~"
        };
        public TextAudioCreator(BSMPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateTextAudio();
        }

        private void CreateTextAudio()
        {
            var textAudios = new List<TextAudios.TextAudio>();
            var textAudioList = _context.TextAudios.ToList();
            //flag 女
            var flagFemalCount = textAudioList.Count(s => s.Scene == TextAudios.ESceneType.Flag && s.Gender == CommonEnum.EGender.F);
            for (int i = 0; i < flagTextAudios.Length; i++)
            {
                var flagTextAudio = textAudioList.FirstOrDefault(r => r.Content == flagTextAudios[i]&&r.Gender==CommonEnum.EGender.F&&r.Scene==TextAudios.ESceneType.Flag);
                if (flagTextAudio == null)
                {
                    flagFemalCount++;
                    textAudios.Add(new TextAudios.TextAudio() { Code=flagFemalCount.ToString("D3"), Content = flagTextAudios[i], Gender = CommonEnum.EGender.F, Scene = TextAudios.ESceneType.Flag, Sort = flagFemalCount });
                }
            }
            //flag  男
            var flagMaleCount = textAudioList.Count(s => s.Scene == TextAudios.ESceneType.Flag && s.Gender == CommonEnum.EGender.M);
            for (int i = 0; i < flagTextAudios.Length; i++)
            {
                var flagTextAudio = textAudioList.FirstOrDefault(r => r.Content == flagTextAudios[i] && r.Gender == CommonEnum.EGender.M && r.Scene == TextAudios.ESceneType.Flag);
                if (flagTextAudio == null)
                {
                    flagMaleCount++;
                    textAudios.Add(new TextAudios.TextAudio() { Code = flagMaleCount.ToString("D3"), Content = flagTextAudios[i], Gender = CommonEnum.EGender.M, Scene = TextAudios.ESceneType.Flag, Sort = flagMaleCount });
                }
            }
            //answer 女
            var answerFemaleCount = textAudioList.Count(s => s.Scene == TextAudios.ESceneType.Flag && s.Gender == CommonEnum.EGender.F);
            for (int i = 0; i < answerTextAudios.Length; i++)
            {
                var answerTextAudio = textAudioList.FirstOrDefault(r => r.Content == answerTextAudios[i] && r.Gender == CommonEnum.EGender.F && r.Scene == TextAudios.ESceneType.Answer);
                if (answerTextAudio == null)
                {
                    answerFemaleCount++;
                    textAudios.Add(new TextAudios.TextAudio() { Code = answerFemaleCount.ToString("D3"), Content = answerTextAudios[i], Gender = CommonEnum.EGender.F, Scene = TextAudios.ESceneType.Answer, Sort = answerFemaleCount });
                }
            }
            //answer  男
            var answerMaleCount = textAudioList.Count(s => s.Scene == TextAudios.ESceneType.Flag && s.Gender == CommonEnum.EGender.M);
            for (int i = 0; i < answerTextAudios.Length; i++)
            {
                var answerTextAudio = textAudioList.FirstOrDefault(r => r.Content == answerTextAudios[i] && r.Gender == CommonEnum.EGender.M && r.Scene == TextAudios.ESceneType.Answer);
                if (answerTextAudio == null)
                {
                    answerMaleCount++;
                    textAudios.Add(new TextAudios.TextAudio() { Code = answerMaleCount.ToString("D3"), Content = answerTextAudios[i], Gender = CommonEnum.EGender.M, Scene = TextAudios.ESceneType.Answer, Sort = answerMaleCount });
                }
            }

            _context.TextAudios.AddRange(textAudios.Distinct());
        }
    }
}
