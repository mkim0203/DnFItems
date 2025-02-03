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
                return $"{BaseInfo.SetPoint} - {DetailInfo.SetsName} ( {DetailInfo.SetsGrade} )";
            }
            return string.Empty;
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
    <td>{item.name}</td>
    <td class='{CodeHelper.GetRarityColor(item.Rarity)}'>{item.Rarity}</td>
    <td>{item.ItemUp}</td>
</tr>";
                    sb.AppendLine(htmlText);
                }
            }
            return sb.ToString();
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
