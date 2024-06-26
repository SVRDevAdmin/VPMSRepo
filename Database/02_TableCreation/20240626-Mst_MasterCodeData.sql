CREATE TABLE `mst_mastercodedata` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `CodeGroup` varchar(20) NOT NULL,
  `CodeID` varchar(20) NOT NULL,
  `CodeName` varchar(20) NOT NULL,
  `Description` varchar(100) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  `SeqOrder` int NOT NULL,
  `CreatedDate` datetime(2) DEFAULT NULL,
  `CreatedBy` varchar(100) DEFAULT NULL,
  `UpdatedDate` datetime(2) DEFAULT NULL,
  `updatedBy` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

Insert into mst_mastercodedata(`CodeGroup`, `CodeID`, `CodeName`, `Description`, `IsActive`, `SeqOrder`, `CreatedDate`, `CreatedBy`) Values
('RoleType', '1', 'Doctor', 'Doctor role', 1, 1, '2024-06-26', 'System'),
('RoleType', '2', 'Clinic Admin', 'Clinic Admin role', 1, 2, '2024-06-26', 'System'),
('RoleType', '3', 'User', 'User role', 1, 3, '2024-06-26', 'System'),
('RoleType', '999', 'Superadmin', 'Superadmin role', 1, 4, '2024-06-26', 'System')