ALTER TABLE `mst_patients_owner`
ADD COLUMN `IsPrimary` INT NULL DEFAULT NULL AFTER `UpdatedBy`;