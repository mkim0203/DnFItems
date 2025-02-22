using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public interface ICharSummary
    {
        string GetCharSummaryHtml();

        string GetSetName();

        string GetSetGrade();

        int GetNextSetPoint();

        int GetSetPoint();

        string GetUseItemTitleSummaryHtml();

        string GetUseItemSummaryHtml();

        FusionItem GetFusionItem();

        string GetCharacterKey();

        string GetServerId();
    }
}
