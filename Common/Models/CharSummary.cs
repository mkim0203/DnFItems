using Common.Models.DfDunDam;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class CharSummary
    {
        public CharSummary(CharInfo charInfo, CharDetailInfo charDetailInfo)
        {
            BaseInfo = charInfo;
            DetailInfo = charDetailInfo;
        }
        private CharInfo BaseInfo {  get; set; }
        private CharDetailInfo DetailInfo { get; set; }


        public string GetCharSummaryHtml()
        {
            if (BaseInfo != null && DetailInfo != null)
            {
                string htmlDoc = $@"<h5 class='card-title'>{BaseInfo.DamageOrBuff}</h5>
<p class='card-text'>{BaseInfo.Job}</p>
<p class='card-text'>Rank : {DetailInfo.Rank}</p>
<p class='card-text'>명성 : {DetailInfo.fame}</p>";
                return htmlDoc;
            }
            return string.Empty;
        }

        public string GetUseItemTitleSummaryHtml()
        {
            if(DetailInfo != null && BaseInfo != null)
            {
                var nextGrade = GetNextGrade(Convert.ToInt32(BaseInfo.SetPoint));
                string nextGradeInfo = string.Empty;
                if (nextGrade != null) {
                    nextGradeInfo = $" / 다음등급 : {nextGrade.Value.Key} 필요포인트({nextGrade.Value.Value - Convert.ToInt32(BaseInfo.SetPoint)})";
                }

                return $"{BaseInfo.SetPoint} - {DetailInfo.SetsName} ( {DetailInfo.SetsGrade} )  {nextGradeInfo}";
            }
            return string.Empty;
        }

        private KeyValuePair<string, int>? GetNextGrade(int setPoint)
        {
            if (setPoint > CodeHelper.RarityCodes.Max(x => x.Value)) { return null; }

            // setpoint 기준 레어리티 정보.
            // setPoint 작은값에서 가장 높은 레어리티 가져옴.
            var closest = CodeHelper.RarityCodes
                .Where(entry => entry.Value > setPoint)
                .OrderBy(entry => entry.Value)
                .FirstOrDefault();

            return closest; // 값이 없으면 기본값 반환
        }

        public string GetUseItemSummaryHtml()
        {
            StringBuilder sb = new StringBuilder();
            if (DetailInfo != null)
            {
                foreach (var item in DetailInfo.UseItems)
                {
                    string htmlText = $@"<tr>
    <td><img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{item.Itemid}'></td>
    <td>{item.Slot}</td>
    <td>{item.ReinforceNum} {item.ReinforceType}</td>
    <td>{item.Name}{(string.IsNullOrWhiteSpace(item.FusionName) == false ? $"<br/>({item.FusionRarity}){item.FusionName}" : "")}</td>
    <td class='{CodeHelper.GetRarityColor(item.Rarity)}'>{item.Rarity}</td>
    <td>{item.ItemUp}</td>
</tr>";

                    sb.AppendLine(htmlText);
                }
            }
            return sb.ToString();
        }

        public FusionItem GetFusionItem()
        {
            FusionItem retValue = new FusionItem();
            if (DetailInfo != null)
            {
                foreach (var item in DetailInfo.UseItems)
                {
                    if (string.IsNullOrWhiteSpace(item.FusionName)) continue;
                        
                    if (CodeHelper.FusionSetNames.Exists(x => item.FusionName.StartsWith(x))) {
                        switch (item.Slot)
                        {
                            case "상의":
                                retValue.Coat = item.FusionRarity == "유니크" ? 25 : 65;
                                break;
                            case "머리어깨":
                                retValue.HandAndShoulder = item.FusionRarity == "유니크" ? 25 : 65;
                                break;
                            case "하의":
                                retValue.Pants = item.FusionRarity == "유니크" ? 25 : 65;
                                break;
                            case "신발":
                                retValue.Shoes = item.FusionRarity == "유니크" ? 25 : 65;
                                break;
                            case "벨트":
                                retValue.Belt = item.FusionRarity == "유니크" ? 25 : 65;
                                break;
                        }
                    }
                    
                }
                  
            }
            return retValue;
        }

        public string GetCharacterKey()
        {
            if(BaseInfo != null) { return BaseInfo.CharacterKey; }
            return string.Empty;
        }

        public string GetServerId()
        {
            if (BaseInfo != null) { return BaseInfo.ServerId; }
            return string.Empty;
        }
    }
}
