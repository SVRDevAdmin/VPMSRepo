CREATE INDEX IX_ViewList
ON Mst_Doctor (Name, Gender, branchID, IsDeleted);

CREATE INDEX IX_ViewList_WithSorting
ON Mst_Doctor (Name, Gender, branchID, IsDeleted, ID);

CREATE INDEX IX_ViewList_SortOrder
ON Mst_Doctor (ID, Name);

CREATE INDEX IX_CodeGroup_CodeID
On Mst_MastercodeData (CodeGroup, CodeID);

CREATE INDEX IX_CodeGroup_CodeID_IsActive
On Mst_MastercodeData (CodeGroup, CodeID, IsActive);

CREATE INDEX IX_CodeGroup
On Mst_MastercodeData (CodeGroup);

CREATE INDEX IX_CodeGroup_IsActive_SeqOrder
On Mst_MastercodeData (CodeGroup, IsActive, SeqOrder);

CREATE INDEX IX_CountryName_IsActive
On Mst_CountryList (CountryName,  IsActive);

CREATE INDEX IX_UserID
On mst_users_configuration (UserID);

CREATE INDEX IX_TemplateIDCode
On mst_template (TemplateID, TemplateCode);

CREATE INDEX IX_TemplateIDLanguageCode
On mst_template_details (TemplateID, LangCode);

/*------- ROLE -----------*/
CREATE INDEX IX_RoleID_IsDeleted
ON Mst_rolepermissions (RoleId, IsDeleted);

CREATE INDEX IX_IsActive
ON Mst_accesspermission (IsActive);

CREATE INDEX IX_RoleID_Status
ON Mst_roles (RoleID, Status);

CREATE INDEX IX_RoleID_RoleName
ON Mst_roles (RoleID, RoleName);

CREATE INDEX IX_RoleID_Branch_Status
ON Mst_roles (RoleID, BranchID, Status);

/*--------- User -------------*/
CREATE INDEX IX_UserViewListing
ON Mst_User (UserID, RoleID, BranchID, STATUS, Gender);

CREATE INDEX IX_Surname_Ordering
ON Mst_User (Surname);

CREATE INDEX IX_UserID_BranchID
ON Mst_User (UserID, BranchID);

CREATE INDEX IX_UserViewListing_Sorting
ON Mst_User (UserID, RoleID, BranchID, STATUS, Gender, Surname);

/*---------- Branch ------------*/
CREATE INDEX IX_OrganizationID
ON Mst_Branch (OrganizationID);

CREATE INDEX IX_OrganizationID_Status
ON Mst_Branch (OrganizationID, Status);

CREATE INDEX IX_BranchID_Status
ON Mst_Branch (ID, Status);

/*---------- Notification --------*/
CREATE INDEX IX_NotiGroup_CreatedDate
On Txn_Notifications (NotificationGroup, CreatedDate DESC);

CREATE INDEX IX_NotiGroup
On Txn_Notifications (NotificationGroup);

CREATE INDEX IX_Notification_OrderSorting
On Txn_Notifications (CreatedDate DESC);

CREATE INDEX IX_NotiID_UserID_Status
On Txn_Notification_Receiver (NotificationID, TargetUser, Status);

CREATE INDEX IX_NotiID_UserID
On Txn_Notification_Receiver (NotificationID, TargetUser);

CREATE INDEX IX_NotifGroup_RoleID
On mst_notification_receiver_config (NotificationGroup, RoleID);

CREATE INDEX IX_NotifGroup
On mst_notification_receiver_config (NotificationGroup);