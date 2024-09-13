CREATE INDEX IX_ViewList
ON Mst_Doctor (Name, Gender, IsDeleted);

CREATE INDEX IX_CodeGroup_CodeID
On Mst_MastercodeData (CodeGroup, CodeID);

CREATE INDEX IX_CodeGroup_CodeID_IsActive
On Mst_MastercodeData (CodeGroup, CodeID, IsActive);