use dtlpension3

Alter Table dbo.MediclaimNonCashless
Drop Constraint DF_MediclaimNonCashless_Active;

Alter Table dbo.MediclaimNonCashless
Drop Column Active;

Alter Table dbo.MediclaimNonCashless
Drop Column RelationWithRetire;

Alter Table dbo.MediclaimNonCashless
Add Organization NVARCHAR(50);

Alter Table dbo.MediclaimNonCashless
Add MedicalCardNo NVARCHAR(20);

Alter Table dbo.MediclaimNonCashless
Add EmailId NVARCHAR(60);

Alter Table dbo.MediclaimNonCashless
Add AccountHolderName NVARCHAR(50);

Alter Table dbo.MediclaimNonCashless
Add AccountNumber NVARCHAR(20);

Alter Table dbo.MediclaimNonCashless
Add BankName NVARCHAR(150);

alter table dbo.MediclaimNonCashless
add BranchName NVARCHAR(100)

Alter Table dbo.MediclaimNonCashless
Add BICCode NVARCHAR(20);

Alter Table dbo.MediclaimNonCashless
Add IFSCNumber NVARCHAR(20);
GO

USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[UpdateMediclaimNonCashless]    Script Date: 29-10-2022 13:50:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
ALTER PROCEDURE [dbo].[UpdateMediclaimNonCashless]  
(  
 @ClaimId INT,  
 @EmployeeNumber NVARCHAR(15),  
 @PPONumber NVARCHAR(10),  
 @MedicalSectionPageNumber INT,   
 @MedicalCardHolderName NVARCHAR(50),  
 @Designation NVARCHAR(50),  
 @PatientName NVARCHAr(50),  
 @Gender NVARCHAR(6),  
 @DateOfBirth DATE,  
 @Age INT,  
 @ClaimFor NVARCHAR(9),  
 @MobileNumber NVARCHAR(10),  
 @CardCategory NVARCHAR(20),  
 @Address NVARCHAR(150),  
 @ClaimType NVARCHAR(3),  
 @MedicalCardPhotoCopy BIT,  
 @PrescriptionDetailPhotoCopy BIT,  
 @OriginalBill BIT,  
 @CashMemo BIT,  
 @ClaimStatusId BIT,  
 @ModifiedBy NVARCHAR(50) NULL,
 @Organization NVARCHAR(50),
 @MedicalCardNo NVARCHAR(20),
 @EmailId NVARCHAR(60),
 @AccountHolderName NVARCHAR(50),
 @AccountNumber NVARCHAR(20),
 @BankName NVARCHAR(150),
 @BranchName NVARCHAR(100),
 @BICCode NVARCHAR(20),
 @IFSCNumber NVARCHAR(20)
)  
As  
BEGIN TRAN  
BEGIN TRY   
  
 UPDATE [dbo].[MediclaimNonCashless]   
  SET  
  EmployeeNumber = @EmployeeNumber  
  ,PPONumber = @PPONumber  
  ,MedicalSectionPageNumber = @MedicalSectionPageNumber   
  ,MedicalCardHolderName = @MedicalCardHolderName  
  ,Designation = @Designation  
  ,PatientName = @PatientName  
  ,Gender = @Gender  
  ,DateOfBirth =  @DateOfBirth  
  ,Age =  @Age  
  ,ClaimFor = @ClaimFor    
  ,MobileNumber = @MobileNumber  
  ,CardCategory = @CardCategory  
  ,[Address] = @Address  
  ,ClaimType = @ClaimType  
  ,MedicalCardPhotoCopy = @MedicalCardPhotoCopy  
  ,PrescriptionDetailPhotoCopy = @PrescriptionDetailPhotoCopy  
  ,OriginalBill = @OriginalBill  
  ,CashMemo =  @CashMemo  
  ,ClaimStatusId =  @ClaimStatusId  
  ,RejectReason = NULL  
  ,ModifiedBy = @ModifiedBy  
  ,ModifiedDate = GETDATE()
  ,Organization = @Organization
  ,MedicalCardNo = @MedicalCardNo
  ,EmailId = @EmailId
  ,AccountHolderName = @AccountHolderName
  ,AccountNumber = @AccountNumber
  ,BankName = @BankName
  ,BranchName = @BranchName
  ,BICCode = @BICCode
  ,IFSCNumber = @IFSCNumber
  WHERE   
   ClaimId = @ClaimId  
  
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


USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[SaveMediclaimNonCashless]    Script Date: 29-10-2022 13:56:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SaveMediclaimNonCashless]
(
	@EmployeeNumber nvarchar(30),
	@PPONumber nvarchar(30),
	@MedicalSectionPageNumber nvarchar(30),	
	@MedicalCardHolderName nvarchar(30),
	@Designation nvarchar(30),
	@PatientName nvarchar(30),
	@Gender nvarchar(6),
	@DateOfBirth date,
	@Age int,
	@ClaimFor nvarchar(9),	
	@MobileNumber nvarchar(15),
	@CardCategory nvarchar(20),
	@Address nvarchar(150),
	@ClaimType nvarchar (3),
	@MedicalCardPhotoCopy bit,
	@PrescriptionDetailPhotoCopy bit ,
	@OriginalBill bit,
	@CashMemo bit,
	@ClaimStatusId int,
	@CreatedBy nvarchar(30) NULL,
	@Organization NVARCHAR(50),
    @MedicalCardNo NVARCHAR(20),
    @EmailId NVARCHAR(60),
    @AccountHolderName NVARCHAR(50),
    @AccountNumber NVARCHAR(20),
    @BankName NVARCHAR(150),
    @BranchName NVARCHAR(100),
    @BICCode NVARCHAR(20),
    @IFSCNumber NVARCHAR(20),
	@ClaimId int output
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[MediclaimNonCashless]
			([EmployeeNumber]
			,[PPONumber]
			,[MedicalSectionPageNumber]	
			,[MedicalCardHolderName]
			,[Designation]
			,[PatientName]
			,[Gender]
			,[DateOfBirth]
			,[Age]
			,[ClaimFor]			
			,[MobileNumber]
			,[CardCategory]
			,[Address]
			,[ClaimType]
			,[MedicalCardPhotoCopy]
			,[PrescriptionDetailPhotoCopy]
			,[OriginalBill]
			,[CashMemo]
			,[ClaimStatusId]
			,[CreatedBy]
			,[Organization]
			,[MedicalCardNo]
			,[EmailId]
			,[AccountHolderName]
			,[AccountNumber]
			,[BankName]
			,[BranchName]
			,[BICCode]
			,[IFSCNumber])
     VALUES
           (@EmployeeNumber
			,@PPONumber
			,@MedicalSectionPageNumber	
			,@MedicalCardHolderName
			,@Designation
			,@PatientName
			,@Gender
			,@DateOfBirth
			,@Age
			,@ClaimFor			
			,@MobileNumber
			,@CardCategory
			,@Address
			,@ClaimType
			,@MedicalCardPhotoCopy
			,@PrescriptionDetailPhotoCopy
			,@OriginalBill
			,@CashMemo
			,@ClaimStatusId
			,@CreatedBy
			,@Organization
			,@MedicalCardNo
			,@EmailId
			,@AccountHolderName
			,@AccountNumber
			,@BankName
			,@BranchName
			,@BICCode
			,@IFSCNumber)
	COMMIT TRAN  
	
	--SELECT @ClaimId = MAX(Id) FROM [dbo].[MediclaimNonCashless];
	 SET @ClaimId = SCOPE_IDENTITY();
	
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

/****** Object:  StoredProcedure [dbo].[GetPendingNonCashlessClaims]    Script Date: 29-10-2022 13:58:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetPendingNonCashlessClaims]    
AS    
BEGIN    
 SELECT [ClaimId]    
      ,[EmployeeNumber]    
      ,[PPONumber]    
      ,[MedicalSectionPageNumber]    
      ,[MedicalCardHolderName]    
      ,[Designation]    
      ,[PatientName]    
      ,[Gender]    
      ,[DateOfBirth]    
      ,[Age]    
      ,[ClaimFor]          
      ,[MobileNumber]    
      ,[CardCategory]    
      ,[Address]    
      ,[ClaimType]    
      ,[MedicalCardPhotoCopy]    
      ,[PrescriptionDetailPhotoCopy]    
      ,[OriginalBill]    
      ,[CashMemo]    
      ,[ClaimStatusId]    
      ,[RejectReason]    
	  ,[PhysicalSubmit]    
	  ,[ApprovedAmount]    
      ,[CreatedBy]    
      ,[CreatedDate]    
      ,[ModifiedBy]    
      ,[ModifiedDate]
	  ,[Organization]
	  ,[MedicalCardNo]
	  ,[EmailId]
	  ,[AccountHolderName]
	  ,[AccountNumber]
	  ,[BankName]
	  ,[BranchName]
	  ,[BICCode]
	  ,[IFSCNumber]
  FROM [dbo].[MediclaimNonCashless]    
  WHERE ClaimStatusId IN (1,2,3)    
END 
GO


USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[GetNonCashlessListByCreatedBy]    Script Date: 29-10-2022 14:01:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetNonCashlessListByCreatedBy]
(
	@CreatedBy nvarchar(30)
)
AS
BEGIN
SELECT [ClaimId]
      ,[EmployeeNumber]
      ,[PPONumber]
      ,[MedicalSectionPageNumber]
      ,[MedicalCardHolderName]
      ,[Designation]
      ,[PatientName]
      ,[Gender]
      ,[DateOfBirth]
      ,[Age]
      ,[ClaimFor]	  
      ,[MobileNumber]
      ,[CardCategory]
      ,[Address]
      ,[ClaimType]
      ,[MedicalCardPhotoCopy]
      ,[PrescriptionDetailPhotoCopy]
      ,[OriginalBill]
      ,[CashMemo]
      ,[ClaimStatusId]
      ,[RejectReason]
	  ,[PhysicalSubmit]
	  ,[ApprovedAmount]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
	  ,[Organization]
	  ,[MedicalCardNo]
	  ,[EmailId]
	  ,[AccountHolderName]
	  ,[AccountNumber]
	  ,[BankName]
	  ,[BranchName]
	  ,[BICCode]
	  ,[IFSCNumber]
  FROM [dbo].[MediclaimNonCashless]
  WHERE
	[CreatedBy] = @CreatedBy
END
GO


USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[GetNonCashlessDetailByClaimId]    Script Date: 29-10-2022 14:02:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetNonCashlessDetailByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT [ClaimId]
      ,[EmployeeNumber]
      ,[PPONumber]
      ,[MedicalSectionPageNumber]
      ,[MedicalCardHolderName]
      ,[Designation]
      ,[PatientName]
      ,[Gender]
      ,[DateOfBirth]
      ,[Age]
      ,[ClaimFor]	  
      ,[MobileNumber]
      ,[CardCategory]
      ,[Address]
      ,[ClaimType]
      ,[MedicalCardPhotoCopy]
      ,[PrescriptionDetailPhotoCopy]
      ,[OriginalBill]
      ,[CashMemo]
      ,[ClaimStatusId]
      ,[RejectReason]
	  ,[PhysicalSubmit]
	  ,[ApprovedAmount]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
	  ,[Organization]
	  ,[MedicalCardNo]
	  ,[EmailId]
	  ,[AccountHolderName]
	  ,[AccountNumber]
	  ,[BankName]
	  ,[BranchName]
	  ,[BICCode]
	  ,[IFSCNumber]
  FROM [dbo].[MediclaimNonCashless]
  WHERE
	[ClaimId] = @ClaimId
END
GO

USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[GetNonCashlessClaimByParam]    Script Date: 29-10-2022 14:03:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetNonCashlessClaimByParam](
	@Date Date = NULL,
	@ClaimId INT = NULL,
	@PageNumber INT = NULL 
)
AS
BEGIN
	SELECT [ClaimId]
      ,[EmployeeNumber]
      ,[PPONumber]
      ,[MedicalSectionPageNumber]
      ,[MedicalCardHolderName]
      ,[Designation]
      ,[PatientName]
      ,[Gender]
      ,[DateOfBirth]
      ,[Age]
      ,[ClaimFor]      
      ,[MobileNumber]
      ,[CardCategory]
      ,[Address]
      ,[ClaimType]
      ,[MedicalCardPhotoCopy]
      ,[PrescriptionDetailPhotoCopy]
      ,[OriginalBill]
      ,[CashMemo]
      ,[ClaimStatusId]
      ,[RejectReason]
	  ,[PhysicalSubmit]
	  ,[ApprovedAmount]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
	  ,[Organization]
	  ,[MedicalCardNo]
	  ,[EmailId]
	  ,[AccountHolderName]
	  ,[AccountNumber]
	  ,[BankName]
	  ,[BranchName]
	  ,[BICCode]
	  ,[IFSCNumber]
  FROM [dbo].[MediclaimNonCashless]
  WHERE 
	(CONVERT(DATE,[CreatedDate],102) = (CONVERT(DATE,@DATE,102)) OR @Date IS NULL)
	AND ((ClaimId = @ClaimId) OR @ClaimId IS NULL)
	AND ((MedicalSectionPageNumber = @PageNumber) OR @PageNumber IS NULL)
END
GO


USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[GetDispensaryByClaimId]    Script Date: 29-10-2022 17:42:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetDispensaryByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT
	   [DispensaryId]
      ,[DispensaryDate]
      ,[DispensaryLocation]
      ,[DoctorVisited]
      ,[TokenOPDNo]
      ,[MedicineAmount]
      ,[Investigation]
      ,[TotalAmount]
	  ,[OtherAmount]
      ,[NonCashlessClaimId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM 
	[dbo].[Dispensary]
  WHERE 
	[NonCashlessClaimId] = @ClaimId
END
GO


USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[GetDispensaryByClaimId]    Script Date: 29-10-2022 17:42:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.DependentInformation (
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(80) NOT NULL,
	[DateOfBirth] DATETIME NOT NULL,
	[Age] INT NOT NULL,
	[RelationWithRetire] NVARCHAR(8),
	[NonCashlessClaimId] INT,
	[Active] BIT DEFAULT(1) NOT NULL,
	[CreatedBy] NVARCHAR(50) NOT NULL,
	[CreatedDate] DATE DEFAULT(GETDATE()) NOT NULL,
	[ModifiedBy] NVARCHAR(50) NULL,
	[ModifiedDate] NVARCHAR(50) NULL,

	CONSTRAINT [FK_ClaimId]  FOREIGN KEY ([NonCashlessClaimId]) REFERENCES dbo.MediclaimNonCashless ([ClaimId])
)
GO

USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[SaveMediclaimNonCashless]    Script Date: 29-10-2022 13:56:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveDependentInformation]
(
	@Name NVARCHAR(80),
	@DateOfBirth DATETIME,
	@Age INT,
	@RelationWithRetire NVARCHAR(8),
	@NonCashlessClaimId INT,
	@CreatedBy NVARCHAR(50)
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[DependentInformation]
			([Name]
			,[DateOfBirth]
			,[Age]	
			,[RelationWithRetire]
			,[NonCashlessClaimId]
			,[CreatedBy])
     VALUES
           (@Name
			,@DateOfBirth
			,@Age	
			,@RelationWithRetire
			,@NonCashlessClaimId
			,@CreatedBy)
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



USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[SaveMediclaimNonCashless]    Script Date: 29-10-2022 13:56:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateDependentInformation]
(
	@Id INT, 
	@Name NVARCHAR(80),
	@DateOfBirth DATETIME,
	@Age INT,
	@RelationWithRetire NVARCHAR(8),
	@ModifiedBy NVARCHAR(50)
)
As
BEGIN TRAN
BEGIN TRY 
	UPDATE [dbo].[DependentInformation] 
		SET [Name] = @Name
			,[DateOfBirth] = @DateOfBirth
			,[Age] = @Age
			,[RelationWithRetire] = @RelationWithRetire
			,[ModifiedBy] = @ModifiedBy
			,[ModifiedDate] = GETDATE()
	WHERE
		Id = @Id
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

USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[SaveMediclaimNonCashless]    Script Date: 29-10-2022 13:56:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetDependentInformationByParam]
(
	@Id INT = NULL, 
	@NonCashlessClaimId INT = NULL,
	@CreatedBy NVARCHAR(50)	= NULL
)
As
BEGIN
	SELECT 
		Id
		,[Name]
		,[DateOfBirth]
		,[Age]
		,[RelationWithRetire]
		,[NonCashlessClaimId]
		,[Active]
		,[CreatedBy]
		,[CreatedDate]
		,[ModifiedBy]
		,[ModifiedDate]
	FROM [dbo].[DependentInformation] 
	WHERE 
	((Id = @Id) OR @Id IS NULL)
	AND ((NonCashlessClaimId = @NonCashlessClaimId) OR @NonCashlessClaimId IS NULL)
	AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)
	COMMIT TRAN 
	
END 
GO

USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[SaveMediclaimNonCashless]    Script Date: 29-10-2022 13:56:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[GetDocumentsByParam]  
(  
 @DocumentId  INT = NULL,  
 @ReferenceId INT = NULL,  
 @ApplicationSubArea NVARCHAR(20) = NULL,
 @Active BIT = 1  
)  
AS  
BEGIN  
  
  SELECT     
   DocumentId   
   ,DocumentPath  
   ,ApplicationArea  
   ,ApplicationSubArea  
   ,ReferenceId  
   ,DocumentFor  
   ,[FileName]  
   ,CreatedBy  
   ,CreatedDate  
   ,ModifiedBy  
   ,ModifiedDate  
  FROM dbo.Document  
  WHERE   
   ((DocumentId = @DocumentId) OR @DocumentId IS NULL)  
   AND ((ApplicationSubArea = @ApplicationSubArea) OR @ApplicationSubArea IS NULL)  
   AND ((ReferenceId = @ReferenceId) OR @ReferenceId IS NULL)  
   AND Active = @Active  
     
END  
GO

USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[SaveMediclaimNonCashless]    Script Date: 29-10-2022 13:56:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
  
  
CREATE PROCEDURE [dbo].[GetDependentInformationByParam]  
(  
 @Id INT = NULL,   
 @NonCashlessClaimId INT = NULL,  
 @CreatedBy NVARCHAR(50) = NULL  
)  
As  
BEGIN  
 SELECT   
  Id  
  ,[Name]  
  ,[DateOfBirth]  
  ,[Age]  
  ,[RelationWithRetire]  
  ,[NonCashlessClaimId]  
  ,[Active]  
  ,[CreatedBy]  
  ,[CreatedDate]  
  ,[ModifiedBy]  
  ,[ModifiedDate]  
 FROM [dbo].[DependentInformation]   
 WHERE   
 ((Id = @Id) OR @Id IS NULL)  
 AND ((NonCashlessClaimId = @NonCashlessClaimId) OR @NonCashlessClaimId IS NULL)  
 AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)  
END   
GO


USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[SaveMediclaimNonCashless]    Script Date: 29-10-2022 13:56:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
  
ALTER TABLE dbo.MediclaimVoucher
ALTER COLUMN PageNo NVARCHAR(10)
GO


USE [dtlpension3]
GO

/****** Object:  StoredProcedure [dbo].[SaveMediclaimVoucher]    Script Date: 29-10-2022 13:56:18 ******/
SET ANSI_NULLS ON
GO

ALTER PROCEDURE [dbo].[SaveMediclaimVoucher]  
(  
 @PayTo [NVARCHAR](30),  
 @ApprovedAmount [MONEY],  
 @AmountInWords [NVARCHAR](100),  
 @EntryNo [INT],  
 @PageNo [NVARCHAR](10),  
 @BankBranch [NVARCHAR](100),  
 @AccountNumber[NVARCHAR](100),  
 @BICCode [NVARCHAR](15),  
 @IFSCCode [NVARCHAR](10),  
 @ClaimId [INT],  
 @ClaimType [NVARCHAR](8) = NULL,  
 @CreatedBy [NVARCHAR](30),  
 @BankName [NVARCHAR](60),  
 @HospitalName [NVARCHAR](60),  
 @VoucherId [INT] OUTPUT  
)  
AS  
BEGIN TRAN  
BEGIN TRY  
 INSERT INTO dbo.MediclaimVoucher  
 (PayTo, ApprovedAmount, AmountInWords, EntryNo, PageNo, BankBranch, AccountNumber, BICCode, IFSCCode, ClaimId, ClaimType, CreatedBy,BankName,HospitalName)  
  VALUES(@PayTo, @ApprovedAmount, @AmountInWords, @EntryNo, @PageNo,  @BankBranch,  @AccountNumber, @BICCode, @IFSCCode, @ClaimId, @ClaimType, @CreatedBy,@BankName,@HospitalName)  
  COMMIT TRAN  
    
  SET @VoucherId = SCOPE_IDENTITY();  
    
END TRY  
BEGIN CATCH  
 ROLLBACK TRAN  
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

/****** Object:  StoredProcedure [dbo].[SaveMediclaimVoucher]    Script Date: 29-10-2022 13:56:18 ******/
SET ANSI_NULLS ON
GO

ALTER PROCEDURE [dbo].[GetMediclaimVoucherByParam]    
(    
 @VoucherId [INT] = NULL,    
 @PayTo [NVARCHAR](30) = NULL,    
 @EntryNo [INT] =NULL,    
 @PageNo [NVARCHAR](10) = NULL,    
 @CreatedBy [NVARCHAR](30) = NULL    
)    
AS    
BEGIN    
 SELECT    
     
  VoucherId,    
  PayTo,    
  ApprovedAmount,    
  AmountInWords,    
  EntryNo,    
  PageNo,    
  BankBranch,    
  AccountNumber,    
  BICCode,    
  IFSCCode,    
  ClaimId,    
  ClaimType,  
  VoucherStatus,  
  BankName,  
  HospitalName  
     
 FROM dbo.MediclaimVoucher    
 WHERE    
  ((VoucherId = @VoucherId) OR @VoucherId IS NULL)    
  AND ((PayTo LIKE '%'+@PayTo+'%') OR @PayTo IS NULL)    
  AND ((EntryNo = @EntryNo) OR @EntryNo IS NULL)    
  AND ((PageNo = @PageNo) OR @PageNo IS NULL)    
  AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)    
  AND (IsDelete = 0)  
END  
