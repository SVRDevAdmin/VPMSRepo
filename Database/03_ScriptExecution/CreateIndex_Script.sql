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