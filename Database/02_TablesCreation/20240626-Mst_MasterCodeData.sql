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
  `UpdatedBy` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `vpmsdb`.`mst_mastercodedata`(`CodeGroup`,`CodeID`,`CodeName`,`Description`,`IsActive`,`SeqOrder`,`CreatedDate`,`CreatedBy`)
VALUES
('RoleType', '1','Doctor','Doctor role',1,1,NOW(),'System'),
('RoleType', '2','Clinic Admin','Clinic Admin role',1,2,NOW(),'System'),
('RoleType', '3','User','User role',1,3,NOW(),'System'),
('RoleType', '999','Superadmin','Superadmin role',1,4,NOW(),'System'),
('Gender','M','Male','Male gender',1,1,NOW(),'System'),
('Gender','F','Female','Female gender',1,2,NOW(),'System'),
('Status','0','Inactive','Inactive Status',1,1,NOW(),'System'),
('Status','1','Active','Active Status',1,2,NOW(),'System'),
('Species','1','Dog','Dog Species',1,1,NOW(),'System'),
('Species','2','Cat','Cat Species',1,2,NOW(),'System'),
('Color','1','Black','Black Color',1,1,NOW(),'System'),
('Color','2','Brown','Brown Color',1,1,NOW(),'System'),
('Color','3','Grey / Silver','Grey / Silver Color',1,1,NOW(),'System'),
('Color','4','Red','Red Color',1,1,NOW(),'System'),
('Color','5','White / Cream','White / Cream Color',1,1,NOW(),'System'),
('Color','6','Yellow / Gold','Yellow / Gold Color',1,1,NOW(),'System'),
('WeightUnit','kg','Kilogram','Kilogram Unit',1,1,NOW(),'System'),
('WeightUnit','g','Gram','Gram Unit',1,2,NOW(),'System'),
('WeightUnit','mg','Miligram','Miligram Unit',1,3,NOW(),'System'),
('HeightUnit','cm','Centimeter','Centimeter Unit',1,1,NOW(),'System'),
('HeightUnit','m','Meter','Meter Unit',1,2,NOW(),'System');
-- ('','','','',,,NOW(),'System')
