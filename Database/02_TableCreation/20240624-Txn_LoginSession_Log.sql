CREATE TABLE `txn_loginsession_log` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ActionType` varchar(100) NOT NULL,
  `SessionID` varchar(40) NOT NULL,
  `SessionCreatedOn` datetime NOT NULL,
  `SessionExpiredOn` datetime NOT NULL,
  `LoginSessionID` bigint NOT NULL,
  `LoginID` varchar(255) NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `CreatedBy` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
