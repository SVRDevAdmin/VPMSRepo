CREATE TABLE `mst_patients_owner` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `PatientID` bigint NOT NULL,
  `Name` varchar(200) NOT NULL,
  `Gender` varchar(2) NOT NULL,
  `ContactNo` varchar(20) NOT NULL,
  `EmailAddress` varchar(50) NOT NULL,
  `Address` varchar(200) NOT NULL,
  `PostCode` varchar(20) NOT NULL,
  `City` varchar(100) NOT NULL,
  `State` varchar(100) NOT NULL,
  `Country` varchar(100) NOT NULL,
  `Status` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `vpmsdb`.`mst_patients_owner`(`PatientID`,`Name`,`Gender`,`ContactNo`,`Address`,`PostCode`,`City`,`State`,`Country`,`Status`,`CreatedDate`,`CreatedBy`)
VALUES
(1,'Choi Hue-Jin','M','0123456789','2-4-24, Pangsapuri Orkid','40460','Shah Alam','Selangor','Malaysia',1,NOW(),'System');
