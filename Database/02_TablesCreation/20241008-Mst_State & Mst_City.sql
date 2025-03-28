CREATE TABLE `mst_state` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `CountryID` int NOT NULL,
  `State` varchar(20) NOT NULL,
  `CreatedDate` datetime(2) DEFAULT NULL,
  `CreatedBy` varchar(100) DEFAULT NULL,
  `UpdatedDate` datetime(2) DEFAULT NULL,
  `UpdatedBy` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `mst_city` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `StateID` int NOT NULL,
  `City` varchar(50) NOT NULL,
  `CreatedDate` datetime(2) DEFAULT NULL,
  `CreatedBy` varchar(100) DEFAULT NULL,
  `UpdatedDate` datetime(2) DEFAULT NULL,
  `UpdatedBy` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;