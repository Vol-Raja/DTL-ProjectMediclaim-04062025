ALTER TABLE AspNetUsers  
ADD IsTempPassword BIT 

ALTER TABLE AspNetUsers 
ADD CONSTRAINT DF_AspNetUsers_IsTempPassword DEFAULT 1 FOR IsTempPassword

UPDATE AspNetUsers SET IsTempPassword = 1