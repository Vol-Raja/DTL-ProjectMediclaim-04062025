
USE [DTLPension]
GO
CREATE TABLE MasterDesignation
(
	DesignationId INT IDENTITY(1,1) NOT NULL,
	DesignationName NVARCHAR(60) NOT NULL,
	DesignationDescription NVARCHAR(60) NULL,
	IsDeleted BIT NOT NULL DEFAULT 0,
	
	CONSTRAINT PK_MasterDesignation_DesignationId PRIMARY KEY (DesignationId),
	CONSTRAINT UQ_MasterDesignation_DesignationName UNIQUE (DesignationName)
);
GO

INSERT INTO dbo.MasterDesignation (DesignationName) VALUES('DA', 'Dealing Assistant')
INSERT INTO dbo.MasterDesignation (DesignationName) VALUES('ASO', 'Assistant Specialist Office')
INSERT INTO dbo.MasterDesignation (DesignationName) VALUES('SO', 'Specialist Officer' )
INSERT INTO dbo.MasterDesignation (DesignationName) VALUES('AM_DM', '' )

GO

USE [DTLPension]
GO
CREATE TABLE MasterDocumentList
(
	DocumentId INT IDENTITY(1,1) NOT NULL,
	DocumentName NVARCHAR(200) NOT NULL,
	IsDeleted BIT NOT NULL DEFAULT 0,
	
	CONSTRAINT PK_MasterDocument_DocumentId PRIMARY KEY (DocumentId),
	CONSTRAINT UQ_MasterDocument_DocumentName UNIQUE (DocumentName)
);
GO

 INSERT INTO MasterDocumentList (DocumentName) VALUES('Photocopy of Medical Card (front and back page)')
 INSERT INTO MasterDocumentList (DocumentName) VALUES('Bill / cash memo (with break-up) of investigation and copy of reports')
 INSERT INTO MasterDocumentList (DocumentName) VALUES('Original Bill')
 INSERT INTO MasterDocumentList (DocumentName) VALUES('Covering Letter')
 INSERT INTO MasterDocumentList (DocumentName) VALUES('Photo of identity - Photocopy of MPC Identity Card')
 INSERT INTO MasterDocumentList (DocumentName) VALUES('Referral Letter')
 INSERT INTO MasterDocumentList (DocumentName) VALUES('Form B - Duly filled and signed by treating doctor countersigned by Medical Superintendent (M.S)')
 INSERT INTO MasterDocumentList (DocumentName) VALUES('Investigations - Name of Test, Code Number and Rates')
 INSERT INTO MasterDocumentList (DocumentName) VALUES('In case of PTCA - Empty Cover/ pouch of the stent with original bill receipt of the Stent')
 INSERT INTO MasterDocumentList (DocumentName) VALUES('In case of Implant - original invoice for single purchase/photocopy of the invoice for bulk purchase with certification from hospital of the implant duly verified with stickers')

GO
USE [DTLPension]
GO
CREATE TABLE MasterDashboard
(
	DashboardId INT IDENTITY(1,1) NOT NULL,
	DashboardName NVARCHAR(100) NOT NULL,
	DashboardFor NVARCHAR(20) NOT NULL,
	IsDeleted BIT NOT NULL DEFAULT 0,
	
	CONSTRAINT PK_MasterDashboard_DashboardId PRIMARY KEY (DashboardId),
	CONSTRAINT UQ_MasterDashboard_DashboardName UNIQUE (DashboardName)
);
GO


INSERT INTO dbo.MasterDashboard (DashboardName, DashboardFor) VALUES ('Dealing Assistant (OPD) Dashboard','Mediclaim');
GO
INSERT INTO dbo.MasterDashboard (DashboardName, DashboardFor) VALUES ('Dealing Assistant (IPD) Dashboard','Mediclaim');
GO
INSERT INTO dbo.MasterDashboard (DashboardName, DashboardFor) VALUES ('Dealing Assistant (Cancer) Dashboard','Mediclaim');  
GO
INSERT INTO dbo.MasterDashboard (DashboardName, DashboardFor) VALUES ('Dealing Assistant (Dialysis) Dashboard','Mediclaim');
GO
INSERT INTO dbo.MasterDashboard (DashboardName, DashboardFor) VALUES ('Dealing Assistant (Dispensary) Dashboard','Mediclaim');
GO
INSERT INTO dbo.MasterDashboard (DashboardName, DashboardFor) VALUES ('AG1/ASO Dashboard','Mediclaim');
GO
INSERT INTO dbo.MasterDashboard (DashboardName, DashboardFor) VALUES ('SO Dashboard','Mediclaim');
GO
INSERT INTO dbo.MasterDashboard (DashboardName, DashboardFor) VALUES ('AM Dashboard','Mediclaim');
GO
INSERT INTO dbo.MasterDashboard (DashboardName, DashboardFor) VALUES ('Mediclaim Disbursment Dashboard','Mediclaim');
GO



USE [DTLPension]
GO 
ALTER TABLE DVBUser
ADD Dashboard NVARCHAR(100)
GO

ALTER TABLE DVBUser
ADD Username NVARCHAR(50)
GO

ALTER TABLE DVBUser
ADD [Address] NVARCHAR(200)
GO

USE [DTLpension]
GO

/****** Object:  StoredProcedure [dbo].[GetArchiveDVBUser]    Script Date: 19-09-2022 11:46:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetArchiveDVBUser]   
As    
BEGIN    
 SELECT  
 ID  
  ,DVBUserId    
  ,[Name]    
  ,EmailAddress    
  ,PhoneNumber    
  ,Department    
  ,Designation    
  ,CreatedBy    
  ,CreatedDate    
  ,ModifiedBy    
  ,ModifiedDate
  ,Dashboard
  ,[Address]
  ,Username
 FROM dbo.DVBUser    
 WHERE
	IsDeleted = 1    
END    
GO


USE [DTLpension]
GO

/****** Object:  StoredProcedure [dbo].[GetDVBUserByParam]    Script Date: 19-09-2022 11:47:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetDVBUserByParam]  
(   
 @Name NVARCHAR(60),  
 @EmailAddress NVARCHAR(120),  
 @Department NVARCHAR(100),  
 @Designation NVARCHAR(50),  
 @DVBUserId INT  
)  
As  
BEGIN  
 SELECT
 ID
  ,DVBUserId  
  ,[Name]  
  ,EmailAddress  
  ,PhoneNumber  
  ,Department  
  ,Designation  
  ,CreatedBy  
  ,CreatedDate  
  ,ModifiedBy  
  ,ModifiedDate
  ,Dashboard
  ,[Address]
  ,Username
 FROM dbo.DVBUser  
 WHERE   
  ((DVBUserId = @DVBUserId) OR @DVBUserId IS NULL)  
 AND (([Name] = @Name) OR @Name IS NULL)  
 AND ((EmailAddress = @EmailAddress) OR @EmailAddress IS NULL)  
 AND ((Department = @Department) OR @Department IS NULL)  
 AND ((Designation =@Designation ) OR @Designation  IS NULL)  
 AND IsDeleted = 0  
END  
GO


USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[SaveDVBUser]    Script Date: 19-09-2022 11:48:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SaveDVBUser]
(
	@ID UNIQUEIDENTIFIER,
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@Department NVARCHAR(100),
	@Designation NVARCHAR(50),
	@CreatedBy NVARCHAR(60),
	@Dashboard NVARCHAR(100),
	@Username NVARCHAR(50),
	@Address NVARCHAR(200),
	@DVBUserId INT OUTPUT
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[DVBUser]
			([ID]
			,[Name]
			,[EmailAddress]	
			,[PhoneNumber]
			,[Department]
			,[Designation]
			,[CreatedBy]
			,Dashboard
			,[Address]
			,Username)
     VALUES
           (@ID
		    ,@Name
			,@EmailAddress
			,@PhoneNumber
			,@Department
			,@Designation
			,@CreatedBy
			,@Dashboard
			,@Address
			,@Username)
	COMMIT TRAN  

	SET @DVBUserId = SCOPE_IDENTITY();
	
END TRY
BEGIN CATCH
   ROLLBACK TRAN
    -- Raise an error and return
    DECLARE @ErrorMessage NVARCHAR(4000);  
    DECLARE @ErrorSeverity INT;  
    DECLARE @ErrorState INT;  
  
    SELECT   
        @ErrorMessage = ERROR_MESSAGE(),  
        @ErrorSeverity = ERROR_SEVERITY(),  
        @ErrorState = ERROR_STATE();  
  
    -- Use RAISERROR inside the CATCH block to return error  
    -- information about the original error that caused  
    -- execution to jump to the CATCH block.  
    RAISERROR (@ErrorMessage, -- Message text.  
               @ErrorSeverity, -- Severity.  
               @ErrorState -- State.  
               );  
END CATCH
GO

USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[UpdateDVBUser]    Script Date: 19-09-2022 11:50:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[UpdateDVBUser]
(	
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@Department NVARCHAR(100),
	@Designation NVARCHAR(50),
	@ModifiedBy NVARCHAR(60),
	@Dashboard NVARCHAR(100),
	@Username NVARCHAR(50),
	@Address NVARCHAR(200),
	@DVBUserId INT
)
AS
BEGIN TRAN
BEGIN TRY 
	UPDATE dbo.DVBUser 
		SET [Name] = @Name,
		EmailAddress = @EmailAddress,
		PhoneNumber = @PhoneNumber,
		Department = @Department,
		Designation = @Designation,
		ModifiedBy = @ModifiedBy,
		ModifiedDate  = GETDATE(),
		Dashboard = @Dashboard,
		[Address] = @Address,
		Username = @Username
	WHERE DVBUserId = @DVBUserId;
	COMMIT TRAN
END TRY
BEGIN CATCH
   ROLLBACK TRAN
    -- Raise an error and return
    DECLARE @ErrorMessage NVARCHAR(4000);  
    DECLARE @ErrorSeverity INT;  
    DECLARE @ErrorState INT;
    SELECT   
        @ErrorMessage = ERROR_MESSAGE(),  
        @ErrorSeverity = ERROR_SEVERITY(),  
        @ErrorState = ERROR_STATE();
    -- Use RAISERROR inside the CATCH block to return error  
    -- information about the original error that caused  
    -- execution to jump to the CATCH block.  
    RAISERROR (@ErrorMessage, -- Message text.  
               @ErrorSeverity, -- Severity.  
               @ErrorState -- State.  
               );  
END CATCH
GO

 