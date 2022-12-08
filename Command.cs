#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.DB.Structure;
using System.Linq;

#endregion

namespace RAB_Session05_Skills
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Building myBuilding = new Building("Office Building", "10 Main Street", 10, 10000);
            myBuilding.Name = "Office Buildng 2";
            Building myBuilding1 = new Building("Hospital", "20 Main Street", 20, 20000);
            Building myBuilding2 = new Building("Apartment Building", "30 Main Street", 30, 30000);
            Building myBuilding3 = new Building("Office Building", "40 Main Street", 40, 40000);

            List<Building> buildingList = new List<Building>();
            buildingList.Add(myBuilding);
            buildingList.Add(myBuilding1);
            buildingList.Add(myBuilding2);
            buildingList.Add(myBuilding3);
            buildingList.Add(new Building("Store", "50 Main Street", 2, 20000));

            Neighborhood neighborhood = new Neighborhood("Downtown", "Boston", "MA", buildingList);

            TaskDialog.Show("Test", "There are " + neighborhood.GetBuildingCount().ToString() 
                + " buildings in" + neighborhood.Name + " " + neighborhood.City);

            Utils.MyStaticMethod();

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Rooms);

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Insert Family");
                foreach (SpatialElement room in collector)
                {
                    SpatialElementBoundaryOptions options = new SpatialElementBoundaryOptions();
                    //IList<BoundarySegment> roomBoundary = room.GetBoundarySegments(options);

                    Location loc = room.Location;
                    LocationPoint locPoint = room.Location as LocationPoint;
                    XYZ roomPoint = locPoint.Point;

                    FamilySymbol myFS = Utils.GetFamilySymbolByName(doc, "Desk", "60in x 30in");
                    FamilyInstance myInstance = doc.Create.NewFamilyInstance(roomPoint, myFS, StructuralType.NonStructural);

                    SetParameterValue(room, "Ceiling Finish", "ACT");
                    string roomName = GetParameterValueAsString(room, "Name");

                    string testString = " this is my string ";
                    string formattedString = testString.Trim();
                }
                t.Commit();
            }

            return Result.Succeeded;
        }

        private void SetParameterValue(Element currentElement, string paramName, string paramValue)
        {
            IList<Parameter> paramList = currentElement.GetParameters(paramName);
            
            //Parameter curParam = currentElement.get_Parameter(BuiltInParameter.ROOM_AREA);

            //if (curParam != null)
                //curParam.Set();

            //paramList.First().Set(paramValue);

            foreach(Parameter param in paramList)
            {
                param.Set(paramValue);
            }
            
        }
        private string GetParameterValueAsString(Element currentElement, string paramName)
        {
            string returnValue = "";

            IList<Parameter> paramList = currentElement.GetParameters(paramName);

            Parameter myParameter = paramList.First();

            returnValue = myParameter.AsString();

            return returnValue;

        }
    }

    public class Neighborhood
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public List<Building> BuildingList { get; set; }

        public Neighborhood(string _name, string _city, string _state, List<Building> buildingList)
        {
            Name = _name;
            City = _city;
            State = _state;
            BuildingList = buildingList;
        }

        public int GetBuildingCount()
        {
            return BuildingList.Count;
        }


    }


}
