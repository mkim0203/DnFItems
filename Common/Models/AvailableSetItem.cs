using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class AvailableSetItem
    {

        public void SetFusionItem(FusionItem item)
        {
            if (item != null)
            {
                FusionHandAndShoulder = item.HandAndShoulder;
                FusionCoat = item.Coat;
                FusionPants = item.Pants;
                FusionBelt = item.Belt;
                FusionShoes = item.Shoes;
            }
        }

        public string SetItemName { get; set; }
        // 머리어깨, 상의, 하의, 벨트, 신발, 팔찌, 목걸이, 보조장비, 반지, 귀걸이, 마법석
        public int HandAndShoulder { get; set; } = 115;
        public int Coat { get; set; } = 115;
        public int Pants { get; set; } = 115;
        public int Belt { get; set; } = 115;
        public int Shoes { get; set; } = 115;

        public int Brac { get; set; } = 115;
        public int Neck { get; set; } = 115;
        public int Sup { get; set; } = 115;
        public int Ring { get; set; } = 115;
        public int Earing { get; set; } = 115;
        public int Ston { get; set; } = 115;

        public int FusionHandAndShoulder { get; set; } = 0;
        public int FusionCoat { get; set; } = 0;
        public int FusionPants { get; set; } = 0;
        public int FusionBelt { get; set; } = 0;
        public int FusionShoes { get; set; } = 0;

        public int AllPoint
        {
            get
            {
                int sum = HandAndShoulder + Coat + Pants + Belt + Shoes + Brac + Neck + Sup + Ring + Earing + Ston
                    + FusionHandAndShoulder + FusionCoat + FusionPants + FusionBelt + FusionShoes;
                return sum;
            }
        }

        public int MaxHandAndShoulder { get; set; } = 145;
        public int MaxCoat { get; set; } = 145;
        public int MaxPants { get; set; } = 145;
        public int MaxBelt { get; set; } = 145;
        public int MaxShoes { get; set; } = 145;
                   
        public int MaxBrac { get; set; } = 145;
        public int MaxNeck { get; set; } = 145;
        public int MaxSup { get; set; } = 145;
        public int MaxRing { get; set; } = 145;
        public int MaxEaring { get; set; } = 145;
        public int MaxSton { get; set; } = 145;
                   
        public int MaxAllPoint
        {
            get
            {
                int sum = MaxHandAndShoulder + MaxCoat + MaxPants + MaxBelt + MaxShoes + MaxBrac + MaxNeck + MaxSup + MaxRing + MaxEaring + MaxSton
                    + FusionHandAndShoulder + FusionCoat + FusionPants + FusionBelt + FusionShoes;
                return sum;
            }
        }

        public string RarityHandAndShoulder { get; set; } = "유니크";
        public string RarityCoat { get; set; } = "유니크";
        public string RarityPants { get; set; } = "유니크";
        public string RarityBelt { get; set; } = "유니크";
        public string RarityShoes { get; set; } = "유니크";
                      
        public string RarityBrac { get; set; } = "유니크";
        public string RarityNeck { get; set; } = "유니크";
        public string RaritySup { get; set; } = "유니크";
        public string RarityRing { get; set; } = "유니크";
        public string RarityEaring { get; set; } = "유니크";
        public string RaritySton { get; set; } = "유니크";

        public int RarityLevelHandAndShoulder { get; set; } = 1;
        public int RarityLevelCoat { get; set; } = 1;
        public int RarityLevelPants { get; set; } = 1;
        public int RarityLevelBelt { get; set; } = 1;
        public int RarityLevelShoes { get; set; } = 1;

        public int RarityLevelBrac { get; set; } = 1;
        public int RarityLevelNeck { get; set; } = 1;
        public int RarityLevelSup { get; set; } = 1;
        public int RarityLevelRing { get; set; } = 1;
        public int RarityLevelEaring { get; set; } = 1;
        public int RarityLevelSton { get; set; } = 1;

        public bool IsTop { get; set; }

        private string GetSetGrade(int setPoint)
        {
            // setpoint 기준 레어리티 정보.
            // setPoint 작은값에서 가장 높은 레어리티 가져옴.
            var closest = CodeHelper.RarityCodes
                .Where(entry => entry.Value < setPoint) 
                .OrderByDescending(entry => entry.Value) 
                .FirstOrDefault(); 

            return closest.Key ?? "레어"; // 값이 없으면 기본값 반환
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



        public void SettingPoint(Common.Models.DfGear.ItemDetail data)
        {
            int setPoint = data.SetPoint.Value;
            // 고유 or 태초 이면 조율 안됨
            int maxSetpoint = (data.ConvertSetItem == "고유" || data.ItemRarity == "태초") ? data.SetPoint.Value : data.SetPoint.Value + 30;
            string itemRarity = data.ItemRarity;

            // // 머리어깨, 상의, 하의, 벨트, 신발, 팔찌, 목걸이, 보조장비, 반지, 귀걸이, 마법석
            switch (data.ItemType)
            {
                case "머리어깨":
                    HandAndShoulder = HandAndShoulder < setPoint ? setPoint : HandAndShoulder;
                    MaxHandAndShoulder = MaxHandAndShoulder < maxSetpoint ? maxSetpoint : MaxHandAndShoulder;
                    if (RarityLevelHandAndShoulder < data.ItemRarityLevel)
                    {
                        RarityHandAndShoulder = itemRarity;
                        RarityLevelHandAndShoulder = data.ItemRarityLevel;
                    }
                    break;
                case "상의":
                    Coat = Coat < setPoint ? setPoint : Coat;
                    MaxCoat = MaxCoat < maxSetpoint ? maxSetpoint : MaxCoat;
                    if (RarityLevelCoat < data.ItemRarityLevel)
                    {
                        RarityCoat = itemRarity;
                        RarityLevelCoat = data.ItemRarityLevel;
                    }
                    break;
                case "하의":
                    Pants = Pants < setPoint ? setPoint : Pants;
                    MaxPants = MaxPants < maxSetpoint ? maxSetpoint : MaxPants;
                    if (RarityLevelPants < data.ItemRarityLevel)
                    {
                        RarityPants = itemRarity;
                        RarityLevelPants = data.ItemRarityLevel;
                    }
                    break;
                case "벨트":
                    Belt = Belt < setPoint ? setPoint : Belt;
                    MaxBelt = MaxBelt < maxSetpoint ? maxSetpoint : MaxBelt;
                    if (RarityLevelBelt < data.ItemRarityLevel)
                    {
                        RarityBelt = itemRarity;
                        RarityLevelBelt = data.ItemRarityLevel;
                    }
                    break;
                case "신발":
                    Shoes = Shoes < setPoint ? setPoint : Shoes;
                    MaxShoes = MaxShoes < maxSetpoint ? maxSetpoint : MaxShoes;
                    if (RarityLevelShoes < data.ItemRarityLevel)
                    {
                        RarityShoes = itemRarity;
                        RarityLevelShoes = data.ItemRarityLevel;
                    }
                    break;
                case "팔찌":
                    Brac = Brac < setPoint ? setPoint : Brac;
                    MaxBrac = MaxBrac < maxSetpoint ? maxSetpoint : MaxBrac;
                    if (RarityLevelBrac < data.ItemRarityLevel)
                    {
                        RarityBrac = itemRarity;
                        RarityLevelBrac = data.ItemRarityLevel;
                    }
                    break;
                case "목걸이":
                    Neck = Neck < setPoint ? setPoint : Neck;
                    MaxNeck = MaxNeck < maxSetpoint ? maxSetpoint : MaxNeck;
                    if (RarityLevelNeck < data.ItemRarityLevel)
                    {
                        RarityNeck = itemRarity;
                        RarityLevelNeck = data.ItemRarityLevel;
                    }
                    break;
                case "보조장비":
                    Sup = Sup < setPoint ? setPoint : Sup;
                    MaxSup = MaxSup < maxSetpoint ? maxSetpoint : MaxSup;
                    if (RarityLevelSup < data.ItemRarityLevel)
                    {
                        RaritySup = itemRarity;
                        RarityLevelSup = data.ItemRarityLevel;
                    }
                    break;
                case "반지":
                    Ring = Ring < setPoint ? setPoint : Ring;
                    MaxRing = MaxRing < maxSetpoint ? maxSetpoint : MaxRing;
                    if (RarityLevelRing < data.ItemRarityLevel)
                    {
                        RarityRing = itemRarity;
                        RarityLevelRing= data.ItemRarityLevel;
                    }
                    break;
                case "귀걸이":
                    Earing = Earing < setPoint ? setPoint : Earing;
                    MaxEaring = MaxEaring < maxSetpoint ? maxSetpoint : MaxEaring;
                    if (RarityLevelEaring < data.ItemRarityLevel)
                    {
                        RarityEaring = itemRarity;
                        RarityLevelEaring = data.ItemRarityLevel;
                    }
                    break;
                case "마법석":
                    Ston = Ston < setPoint ? setPoint : Ston;
                    MaxSton = MaxSton < maxSetpoint ? maxSetpoint : MaxSton;
                    if (RarityLevelSton < data.ItemRarityLevel)
                    {
                        RaritySton = itemRarity;
                        RarityLevelSton = data.ItemRarityLevel;
                    }
                    break;
            }
        }

        public override string ToString()
        {
            string retValue =
$@"{SetItemName}
  - 기    본 : {HandAndShoulder} {Coat} {Pants} {Belt} {Shoes} // {Brac} {Neck} {Sup} {Ring} {Earing} {Ston} | {AllPoint} | {GetSetGrade(AllPoint)}
  - 조율최대 : {MaxHandAndShoulder} {MaxCoat} {MaxPants} {MaxBelt} {MaxShoes} // {MaxBrac} {MaxNeck} {MaxSup} {MaxRing} {MaxEaring} {MaxSton} | {MaxAllPoint} | {GetSetGrade(MaxAllPoint)}";
            return retValue;
        }

        public string OutputHtml()
        {
            var nextGradeAllPoint = GetNextGrade(AllPoint);
            var nextGradeMaxAllPoint = GetNextGrade(MaxAllPoint);

            string retValue =
$@"<tr>
    <th rowspan='2'>{SetItemName}</th>
    <td>기    본</td>
    <td class='{CodeHelper.GetRarityColor(RarityHandAndShoulder)}'>{HandAndShoulder}</td>
    <td class='{CodeHelper.GetRarityColor(RarityCoat)}'>{Coat}</td>
    <td class='{CodeHelper.GetRarityColor(RarityPants)}'>{Pants}</td>
    <td class='{CodeHelper.GetRarityColor(RarityBelt)}'>{Belt}</td>
    <td class='{CodeHelper.GetRarityColor(RarityShoes)}'>{Shoes}</td>
    <td class='{CodeHelper.GetRarityColor(RarityBrac)}'>{Brac}</td>
    <td class='{CodeHelper.GetRarityColor(RarityNeck)}'>{Neck}</td>
    <td class='{CodeHelper.GetRarityColor(RaritySup)}'>{Sup}</td>
    <td class='{CodeHelper.GetRarityColor(RarityRing)}'>{Ring}</td>
    <td class='{CodeHelper.GetRarityColor(RarityEaring)}'>{Earing}</td>
    <td class='{CodeHelper.GetRarityColor(RaritySton)}'>{Ston}</td>

    <td class='{CodeHelper.GetFusionRarityColor(FusionHandAndShoulder)}'>{FusionHandAndShoulder}</td>
    <td class='{CodeHelper.GetFusionRarityColor(FusionCoat)}'>{FusionCoat}</td>
    <td class='{CodeHelper.GetFusionRarityColor(FusionPants)}'>{FusionPants}</td>
    <td class='{CodeHelper.GetFusionRarityColor(FusionBelt)}'>{FusionBelt}</td>
    <td class='{CodeHelper.GetFusionRarityColor(FusionShoes)}'>{FusionShoes}</td>

    <td{(IsTop ? " class='table-primary'" : "")}>{AllPoint}{(IsTop ? "(*)" : "")}</td>
    <td class='{CodeHelper.GetRarityColor(GetSetGrade(AllPoint))}'>{GetSetGrade(AllPoint)}</td>
    <td>{(nextGradeAllPoint == null ? "" : (nextGradeAllPoint.Value.Value - AllPoint).ToString())}</td>
</tr>
<tr>
    <td>조율최대</td>
    <td class='{CodeHelper.GetRarityColor(RarityHandAndShoulder)}'>{MaxHandAndShoulder}</td>
    <td class='{CodeHelper.GetRarityColor(RarityCoat)}'>{MaxCoat}</td>
    <td class='{CodeHelper.GetRarityColor(RarityPants)}'>{MaxPants}</td>
    <td class='{CodeHelper.GetRarityColor(RarityBelt)}'>{MaxBelt}</td>
    <td class='{CodeHelper.GetRarityColor(RarityShoes)}'>{MaxShoes}</td>
    <td class='{CodeHelper.GetRarityColor(RarityBrac)}'>{MaxBrac}</td>
    <td class='{CodeHelper.GetRarityColor(RarityNeck)}'>{MaxNeck}</td>
    <td class='{CodeHelper.GetRarityColor(RaritySup)}'>{MaxSup}</td>
    <td class='{CodeHelper.GetRarityColor(RarityRing)}'>{MaxRing}</td>
    <td class='{CodeHelper.GetRarityColor(RarityEaring)}'>{MaxEaring}</td>
    <td class='{CodeHelper.GetRarityColor(RaritySton)}'>{MaxSton}</td>
   
    <td class='{CodeHelper.GetFusionRarityColor(FusionHandAndShoulder)}'>{FusionHandAndShoulder}</td>
    <td class='{CodeHelper.GetFusionRarityColor(FusionCoat)}'>{FusionCoat}</td>
    <td class='{CodeHelper.GetFusionRarityColor(FusionPants)}'>{FusionPants}</td>
    <td class='{CodeHelper.GetFusionRarityColor(FusionBelt)}'>{FusionBelt}</td>
    <td class='{CodeHelper.GetFusionRarityColor(FusionShoes)}'>{FusionShoes}</td>

    <td>{MaxAllPoint}</td>
    <td class='{CodeHelper.GetRarityColor(GetSetGrade(MaxAllPoint))}'>{GetSetGrade(MaxAllPoint)}</td>
    <td>{(nextGradeMaxAllPoint == null ? "" : (nextGradeMaxAllPoint.Value.Value - MaxAllPoint).ToString())}</td>
</tr>";
            return retValue;
        }

    }
}
