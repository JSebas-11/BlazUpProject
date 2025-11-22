namespace Domain.Common;

//Tamaños minimos y maximos para los campos definidos en la DB y otras
//limitaciones de reglas de negocio
public static class ValidationConsts {
    #region LookUpTables
    //UserRole-RequirementType-StateNotification-StateEntity-LevelPriority
    public const int MinLookUpDescriptionLength = 4;
    public const int MaxLookUpDescriptionLength = 48;
    #endregion
    
    #region Tables
    //Common Columns
    public const int MinEntityNameLength = 4;
    public const int MaxEntityNameLength = 64;
    public const int MinEntityDescriptionLength = 5;
    public const int MaxEntityDescriptionLength = 512;
    public const decimal MinEntityProgressLength = 0;
    public const decimal MaxEntityProgressLength = 100;
    
    //UserInfo
    public const int MinDniLength = 10;
    public const int MaxDniLength = 13;
    public const int MinUserNameLength = 4;
    public const int MaxUserNameLength = 64;
    public const int MinPasswordLength = 5;
    public const int MaxPasswordLength = 30;
    public const string PasswordPattern = @"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{5,30}$";

    //NotificationApp
    public const int MinTitleLength = 4;
    public const int MaxTitleLength = 64;
    public const int MinMessageLength = 4;
    public const int MaxMessageLength = 128;
    #endregion
}

