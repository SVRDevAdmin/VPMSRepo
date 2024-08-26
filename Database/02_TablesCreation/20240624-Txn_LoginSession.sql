CREATE TABLE `txn_loginsession` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `SessionID` varchar(40) NOT NULL,
  `SessionCreatedOn` datetime NOT NULL,
  `SessionExpiredOn` datetime NOT NULL,
  `UserID` varchar(255) NOT NULL,
  `LoginID` varchar(255) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;