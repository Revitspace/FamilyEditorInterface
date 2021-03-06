#region Namespaces
using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using FamilyEditorInterface.Associate;
#endregion

namespace FamilyEditorInterface
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalCommand
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Command : IExternalCommand
    {
        public static String global_message;

        public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (Application.Started) return Result.Succeeded;
            try
            {
                Application.App = commandData.Application;
                if (Application.Control.ShowForm()) //If the Document is not a FamilyDocument, the app shouldn't start
                {
                    Application.Started = true;
                    return Result.Succeeded;
                }
                else
                {
                    return Result.Failed;
                }
            }
            catch (Exception ex)
            {
                message += global_message;
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
    /// <summary>
    /// Implements the Revit add-in interface IExternalCommand
    /// Associates parameter between two families
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class CommandAssociateParameters : IExternalCommand
    {
        public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Autodesk.Revit.ApplicationServices.Application App = uiapp.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if(!Utils.CheckFamilyDocument(doc)) return Result.Cancelled;    //The method should only be used inside a FamilyDocument

            WireParameters wire = new WireParameters(uidoc, doc);
            wire.Wire();

            return Result.Succeeded;
        }
    }
    /// <summary>
    /// Implements the Revit add-in interface IExternalCommand
    /// Push parameters from the current family into a nested family
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class CommandPushParameters : IExternalCommand
    {
        public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            
            if (!Utils.CheckFamilyDocument(doc)) return Result.Cancelled;    //The method should only be used inside a FamilyDocument

            PushParameters push = new PushParameters(app, uidoc, doc);
            push.Push();

            return Result.Succeeded;
        }
    }
    /// <summary>
    /// Implements the Revit add-in interface IExternalCommand
    /// Pulls Parameters from a nested family into the current family
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class CommandPullParameters : IExternalCommand
    {
        public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (!Utils.CheckFamilyDocument(doc)) return Result.Cancelled;    //The method should only be used inside a FamilyDocument

            PullParameters pull = new PullParameters(app, uidoc, doc);
            pull.Pull();

            return Result.Succeeded;
        }
    }
}
