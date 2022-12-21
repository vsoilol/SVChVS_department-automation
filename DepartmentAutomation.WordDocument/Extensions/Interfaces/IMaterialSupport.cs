using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    public interface IMaterialSupport
    {
        void PasteInfoAboutMaterialSupport(Body body, IReadOnlyList<Audience> audiences);
    }
}