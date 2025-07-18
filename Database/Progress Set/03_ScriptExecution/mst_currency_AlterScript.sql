ALTER TABLE `txn_customer_expenses_summary`
ADD COLUMN `EntityType` VARCHAR(50) NULL DEFAULT NULL AFTER `UpdatedBy`;

ALTER TABLE `mst_currency`
ADD COLUMN `CurrencyCode` VARCHAR(50) NULL DEFAULT NULL AFTER `Country`,
ADD COLUMN `DisplayFormat` VARCHAR(50) NULL DEFAULT NULL AFTER `CurrencySymbol`;
