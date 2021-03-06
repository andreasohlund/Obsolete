using System;
using System.Linq;

public partial class ModuleWeaver
{
    public string TreatAsErrorFormat = "Will be treated as an error from version {0}. ";
    public StepType StepType = StepType.Major;
    public string RemoveInVersionFormat = "Will be removed in version {0}.";
    public string ReplacementFormat = "Please use `{0}` instead. ";

    public bool HideObsoleteMembers = true;

    public void ReadConfig()
    {
        if (Config == null)
        {
            return;
        }
        ReadFormats();
        ReadHideObsoleteMembers();
        ReadVersionIncrement();
        ReadStepType();
    }

    void ReadFormats()
    {
        var treatAsErrorFormat = Config.Attributes("TreatAsErrorFormat").FirstOrDefault();
        if (treatAsErrorFormat != null)
        {
            TreatAsErrorFormat = treatAsErrorFormat.Value;
        }

        var removeInVersionFormat = Config.Attributes("RemoveInVersionFormat").FirstOrDefault();
        if (removeInVersionFormat != null)
        {
            RemoveInVersionFormat = removeInVersionFormat.Value;
        }
        var replacementFormat = Config.Attributes("ReplacementFormat").FirstOrDefault();
        if (replacementFormat != null)
        {
            ReplacementFormat = replacementFormat.Value;
        }
    }

    void ReadHideObsoleteMembers()
    {
        var xAttribute = Config.Attribute("HideObsoleteMembers");
        if (xAttribute != null)
        {
            if (!bool.TryParse(xAttribute.Value, out HideObsoleteMembers))
            {
                throw new Exception(string.Format("Could not parse 'HideObsoleteMembers' from '{0}'.", xAttribute.Value));
            }
        }
    }
    
    void ReadVersionIncrement()
    {
        var xAttribute = Config.Attribute("VersionIncrement");
        if (xAttribute != null)
        {
            throw new WeavingException("VersionIncrement is no longer supported. Use StepType instead.");
        }
    }

    void ReadStepType()
    {
        var xAttribute = Config.Attribute("StepType");
        if (xAttribute != null)
        {
            if (!Enum.TryParse(xAttribute.Value, out StepType))
            {
                throw new Exception(string.Format("Could not parse 'StepType' from '{0}'.", xAttribute.Value));
            }
        }
    }
}