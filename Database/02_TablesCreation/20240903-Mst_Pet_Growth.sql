CREATE TABLE `mst_pet_growth` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `PetID` int DEFAULT NULL,
  `Age` int DEFAULT NULL,
  `Height` decimal(6,2) NOT NULL,
  `Weight` decimal(6,2) NOT NULL,
  `Allergies` varchar(150) DEFAULT NULL,
  `BMI` decimal(6,2) NOT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
