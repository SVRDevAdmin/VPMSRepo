CREATE TABLE `mst_pets_breed` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `Species` varchar(50) NOT NULL,
  `Breed` varchar(100) NOT NULL,
  `Active` int NOT NULL,
  `SeqOrder` int NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


INSERT INTO `mst_pets_breed`(`Species`,`Breed`,`Active`,`SeqOrder`,`CreatedDate`,`CreatedBy`)
VALUES
('Dog','Alaskan Malamute',1,1,NOW(),'System'),
('Dog','Afghan Hound',1,2,NOW(),'System'),
('Dog','Bulldog',1,3,NOW(),'System'),
('Dog','Boxer',1,4,NOW(),'System'),
('Dog','Chow Chow',1,5,NOW(),'System'),
('Dog','German Shepherd',1,6,NOW(),'System'),
('Dog','Australian Shepherd',1,7,NOW(),'System'),
('Dog','French Bulldog',1,8,NOW(),'System'),
('Dog','Golden Retriever',1,9,NOW(),'System'),
('Dog','Pomeranian',1,10,NOW(),'System'),
('Dog','Chihuahua',1,11,NOW(),'System'),
('Dog','Sussex Spaniel',1,12,NOW(),'System'),
('Dog','American Bulldog',1,13,NOW(),'System'),
('Dog','Rottweiler',1,14,NOW(),'System'),
('Cat','Siamese cat',1,1,NOW(),'System'),
('Cat','Persian cat',1,2,NOW(),'System'),
('Cat','Abyssinian',1,3,NOW(),'System'),
('Cat','Bombay cat',1,4,NOW(),'System'),
('Cat','Scottish Fold',1,5,NOW(),'System'),
('Cat','Birman',1,6,NOW(),'System'),
('Cat','Siberian cat',1,7,NOW(),'System'),
('Cat','Chartreux',1,8,NOW(),'System');
