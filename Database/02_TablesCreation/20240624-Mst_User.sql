CREATE TABLE `vpmsdb`.`mst_user` (
  `UserID` VARCHAR(255) NOT NULL,
  `Name` VARCHAR(200) NOT NULL,
  `EmailAddress` VARCHAR(100) NOT NULL,
  `Status` INT NOT NULL,
  `RoleID` VARCHAR(255) NOT NULL,
  `BranchID` INT NOT NULL,
  `LastLoginDate` DATETIME NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`UserID`)
  )ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;