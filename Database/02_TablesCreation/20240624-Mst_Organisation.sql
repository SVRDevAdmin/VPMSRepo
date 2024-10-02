CREATE TABLE `mst_organisation` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Level` int NOT NULL,
  `ParentID` int NOT NULL,
  `Name` varchar(200) NOT NULL,
  `TotalStaff` int NOT NULL,
  `Status` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
