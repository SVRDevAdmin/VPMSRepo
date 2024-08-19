CREATE TABLE `mst_pets` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `PatientID` bigint NOT NULL,
  `Name` varchar(100) NOT NULL,
  `RegistrationNo` varchar(50) NOT NULL,
  `Gender` varchar(2) NOT NULL,
  `DOB` date NOT NULL,
  `Age` int NOT NULL,
  `Species` varchar(10) NOT NULL,
  `Breed` varchar(100) NOT NULL,
  `Color` varchar(10) NOT NULL,
  `Allergies` varchar(150) NOT NULL,
  `Weight` decimal(6,2) NOT NULL,
  `WeightUnit` varchar(5) NOT NULL,
  `Height` decimal(6,2) NOT NULL,
  `HeightUnit` varchar(5) NOT NULL,
  `Status` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `vpmsdb`.`mst_pets`(`PatientID`,`Name`,`RegistrationNo`,`Gender`,`DOB`,`Age`,`Species`,`Breed`,`Color`,`Allergies`,`Weight`,`WeightUnit`,`Height`,`HeightUnit`,`Status`,`CreatedDate`,`CreatedBy`)
VALUES
(1,'Simba','C01','M','2023-01-01',1,'Cat','Siamese cat','Grey / Silver',null,5.2,'kg',0.5,'m',1,NOW(),'System');