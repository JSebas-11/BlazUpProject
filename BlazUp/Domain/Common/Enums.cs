namespace Domain.Common;

//Enums con relacion indirecta a sus relativos en la DB (valores fijos)
public enum Role { Admin = 1, Client, Developer }
public enum RequirementType { FixBug = 1, NewFeature, Change, Refactor, Testing, Design, Documentation, Other }
public enum StateNotification { UnRead = 1, Read }
public enum StateEntity { Pending = 1, InProgress, Postponed, Finished, Completed }
public enum LevelPriority { VeryLow = 1, Low, Normal, High, Critical }
