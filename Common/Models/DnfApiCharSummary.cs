using Common.Models.DnfApi;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class DnfApiCharSummary : ICharSummary
    {
        private CharInfo _baseInfo { get; set; }
        private EquipmentResult _equiInfos { get; set; }

        public DnfApiCharSummary(CharInfo charInfo, EquipmentResult equipment)
        {
            if(charInfo != null) this._baseInfo = charInfo;
            if (equipment != null)
            {
                this._equiInfos = equipment;

                foreach (var item in this._equiInfos.EquipmentInfos)
                {
                    int targetOrder = -1;
                    bool findItem = CodeHelper.SlotOrder.TryGetValue(item.SlotName, out targetOrder);
                    item.SlotOrder = targetOrder;
                }
            }
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

        public string GetCharacterKey()
        {
            if (_baseInfo != null) return _baseInfo.CharacterId;
            return string.Empty;
        }

        public string GetServerId()
        {
            if(_baseInfo != null) return _baseInfo.ServerId;
            return string.Empty;
        }

        public string GetCharSummaryHtml()
        {
            if (_baseInfo != null)
            {
                string htmlDoc = $@"<h5 class='card-title'></h5>
<p class='card-text'>{_baseInfo.JobGrowName}</p>
<p class='card-text'>명성 : {_baseInfo.Fame}</p>";
                return htmlDoc;
            }
            
            return string.Empty;
        }

        public FusionItem GetFusionItem()
        {
            FusionItem retValue = new FusionItem();
            if (_equiInfos != null && _equiInfos.EquipmentInfos != null)
            {
                foreach (var item in _equiInfos.EquipmentInfos)
                {
                    if (item.UpgradeInfo == null || item.UpgradeInfo.SetItemName == null) continue;

                    if (CodeHelper.SetItems.Exists(x => item.UpgradeInfo.SetItemName.Equals(x)))
                    {
                        switch (item.SlotName)
                        {
                            case "상의":
                                retValue.Coat = item.UpgradeInfo.ItemRarity == "유니크" ? 25 : 65;
                                break;
                            case "머리어깨":
                                retValue.HandAndShoulder = item.UpgradeInfo.ItemRarity == "유니크" ? 25 : 65;
                                break;
                            case "하의":
                                retValue.Pants = item.UpgradeInfo.ItemRarity == "유니크" ? 25 : 65;
                                break;
                            case "신발":
                                retValue.Shoes = item.UpgradeInfo.ItemRarity == "유니크" ? 25 : 65;
                                break;
                            case "벨트":
                                retValue.Belt = item.UpgradeInfo.ItemRarity == "유니크" ? 25 : 65;
                                break;
                        }
                    }

                }

            }
            return retValue;
        }

        private SetItemInfoItem GetMaxSetItemInfoItem()
        {
            if (_equiInfos?.SetItemInfos?.Count > 0)
            {
                if (_equiInfos.SetItemInfos.Count == 1) return _equiInfos.SetItemInfos.FirstOrDefault();

                int maxSetPoint = _equiInfos.SetItemInfos.Max(x => x.Active.SetPoint.Current);
                return _equiInfos.SetItemInfos.FirstOrDefault(x => x.Active.SetPoint.Current == maxSetPoint);
            }

            return null;
        }

        public int GetNextSetPoint()
        {
            var target = GetMaxSetItemInfoItem();
            if (target?.Active?.SetPoint != null)
            {
                int setPoint = target.Active.SetPoint.Current;
                var nextGrade = GetNextGrade(setPoint);

                if (nextGrade.Value.Value == 0) return 0;
                return nextGrade.Value.Value - setPoint;
            }
           
            return 0;
        }


        public string GetSetGrade()
        {
            var target = GetMaxSetItemInfoItem();
            if (target != null)
            {
                return target.SetItemRarityName;
            }

            return string.Empty;
        }

        public string GetSetName()
        {
            var target = GetMaxSetItemInfoItem();
            if (target != null)
            {
                return target.SetItemName;
            }

            return string.Empty;
        }

        public string GetUseItemSummaryHtml()
        {
            StringBuilder sb = new StringBuilder();
            if (_equiInfos != null)
            {
                foreach (var item in _equiInfos.EquipmentInfos.OrderBy(x => x.SlotOrder))
                {
                    string htmlText = $@"<tr>
    <td><img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{item.ItemId}'></td>
    <td>{item.SlotName}</td>
    <td>{item.Reinforce} {(string.IsNullOrWhiteSpace(item.AmplificationName) ? "강화" : "증폭")}</td>
    <td>{item.ItemName}{(item.UpgradeInfo != null ? $"<br/>({item.UpgradeInfo.ItemRarity}){item.UpgradeInfo.ItemName}" : "")}</td>
    <td class='{CodeHelper.GetRarityColor(item.ItemRarity)}'>{item.ItemRarity}</td>
    <td>{item.TuneInfo?.Level}</td>
</tr>";

                    sb.AppendLine(htmlText);
                }
            }
            return sb.ToString();
        }

        public string GetUseItemTitleSummaryHtml()
        {
            var target = GetMaxSetItemInfoItem();
            if (target != null)
            {
                int setPoint = target.Active.SetPoint.Current;
                var nextGrade = GetNextGrade(setPoint);

                string nextGradeInfo = string.Empty;
                if (nextGrade != null)
                {
                    nextGradeInfo = $" / 다음등급 : {nextGrade.Value.Key} 필요포인트({GetNextSetPoint()})";
                }

                return $"{setPoint} - {target.SetItemName} ( {target.SetItemRarityName} )  {nextGradeInfo}";
            }
            return string.Empty;
        }

        public int GetSetPoint()
        {
            var target = GetMaxSetItemInfoItem();
            if (target?.Active?.SetPoint != null)
            {
                int setPoint = target.Active.SetPoint.Current;
                return setPoint;
            }
            return 0;
        }
    }
}
