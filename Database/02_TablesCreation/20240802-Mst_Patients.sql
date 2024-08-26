CREATE TABLE `mst_patients` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `BranchID` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `vpmsdb`.`mst_patients`(`BranchID`,`CreatedDate`,`CreatedBy`)
VALUES
(1,NOW(),'System');