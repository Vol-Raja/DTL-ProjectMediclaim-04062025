-----------------------------------------------------
--Tables/Procedure for medicliam raise and processing
-----------------------------------------------------

-----DROP COMMAND-----

DROP TABLE [dbo].[ClaimStatusMaster]
DROP TABLE [dbo].[ClaimTypeMaster]

DROP TABLE [dbo].[Dispensary]
DROP PROCEDURE [dbo].[SaveDispensary]
DROP PROCEDURE [dbo].[GetDispensaryByClaimId]

DROP TABLE [dbo].[IPD]
DROP PROCEDURE [dbo].[SaveIPD]
DROP PROCEDURE [dbo].[GetIPDByClaimId]

DROP TABLE [dbo].[OPDCND]
DROP PROCEDURE [dbo].[SaveOPDCND]
DROP PROCEDURE [dbo].[GetOPDCNDByClaimId]

DROP TABLE [dbo].[MediclaimNonCashless]
DROP PROCEDURE [dbo].[SaveMediclaimNonCashless]
DROP PROCEDURE [dbo].[ApproveRejectNonCashlessClaim]
DROP PROCEDURE [dbo].[GetNonCashlessDetailByClaimId]
DROP PROCEDURE [dbo].[GetNonCashlessListByCreatedBy]
DROP PROCEDURE [dbo].[GetPendingNonCashlessClaims]
DROP PROCEDURE [dbo].[GetNonCashlessClaimByParam]


-----CLAIM TYPE MASTER-----
----------START------------
CREATE TABLE dbo.ClaimTypeMaster
(
	[Id] INT PRIMARY KEY NOT NULL,
	[Type] NVARCHAR(11) NOT NULL,
)

IF NOT EXISTS(SELECT 1 FROM dbo.ClaimTypeMaster WHERE [Type] ='Cashless')
BEGIN
INSERT INTO dbo.ClaimTypeMaster VALUES (1, 'Cashless') 
END
GO

IF NOT EXISTS(SELECT 1 FROM dbo.ClaimTypeMaster WHERE [Type] ='Nonashless')
BEGIN
INSERT INTO dbo.ClaimTypeMaster VALUES (2, 'NonCashless') 
END
GO

----------END------------

CREATE PROCEDURE GetMedilaimsForReportByParam
(
	@ClaimId INT = NULL,
	@ClaimStatusId INT = NULL,
	@ClaimType NVARCHAR(11) = NULL,
	@FromDate DATE = NULL,
	@ToDate DATE = NULL
)
AS
IF(@ClaimType = 'Cashless')
	BEGIN
		SELECT 
			ClaimId
			,MedicalSectionPageNumber
			,NameOfPatient as PatientName
			,'Cashless' as ApplicationType
			,CreatedDate
			,ApprovedAmount
		FROM dbo.MediclaimCashless
		WHERE 
			((ClaimStatusId = @ClaimStatusId) OR @ClaimStatusId IS NULL)
			AND((ClaimId = @ClaimId) OR @ClaimId IS NULL)
			AND((CONVERT(DATE,CreatedDate,102) BETWEEN CONVERT(DATE,@FromDate,102) AND CONVERT(DATE,@ToDate,102)) OR (@FromDate IS NULL AND @ToDate IS NULL))
	END
ELSE
	BEGIN
		SELECT 
			ClaimId
			,MedicalSectionPageNumber
			,PatientName as PatientName
			,'Noncashless' as ApplicationType
			,CreatedDate
			,ApprovedAmount
		FROM dbo.MediclaimNonCashless
		WHERE 
			((ClaimStatusId = @ClaimStatusId) OR @ClaimStatusId IS NULL)
			AND((ClaimId = @ClaimId) OR @ClaimId IS NULL)
			AND((CONVERT(DATE,CreatedDate,102) BETWEEN CONVERT(DATE,@FromDate,102) AND CONVERT(DATE,@ToDate,102)) OR (@FromDate IS NULL AND @ToDate IS NULL))
	END

--select * from dbo.MediclaimCashless

-----CLAIM STATUS MASTER-----
-----------START-------------
CREATE TABLE dbo.ClaimStatusMaster
(
	[Id] INT PRIMARY KEY NOT NULL,
	[Status] NVARCHAR(10) NOT NULL
)


IF NOT EXISTS(SELECT 1 FROM dbo.ClaimStatusMaster WHERE [Status] ='Pending')
BEGIN
INSERT INTO dbo.ClaimStatusMaster VALUES (1, 'Pending') 
END
GO

IF NOT EXISTS(SELECT 1 FROM dbo.ClaimStatusMaster WHERE [Status] ='Approve')
BEGIN
INSERT INTO dbo.ClaimStatusMaster VALUES (2, 'Approve') 
END
GO

IF NOT EXISTS(SELECT 1 FROM dbo.ClaimStatusMaster WHERE [Status] ='Reject')
BEGIN
INSERT INTO dbo.ClaimStatusMaster VALUES (3, 'Reject') 
END
GO

-----------END-------------

------MEDICLAIM NON CASHLESS TABLE------
-----------------START------------------

CREATE TABLE [dbo].[MediclaimNonCashless](
	[ClaimId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] [nvarchar](15) NOT NULL,
	[PPONumber] [nvarchar](10) NOT NULL,
	[MedicalSectionPageNumber] [int] NOT NULL,	
	[MedicalCardHolderName] [nvarchar](30) NOT NULL,
	[Designation] [nvarchar](30) NOT NULL,
	[PatientName] [nvarchar](30) NOT NULL,
	[Gender] [nvarchar](6) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Age] [int] NOT NULL,
	[ClaimFor] [nvarchar](9) NOT NULL,
	[RelationWithRetire] [nvarchar](8) NOT NULL,
	[MobileNumber] [nvarchar](10) NOT NULL,
	[CardCategory] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](60) NOT NULL,
	[ClaimType] [nvarchar](3) NOT NULL,
	[MedicalCardPhotoCopy] [bit] NOT NULL,
	[PrescriptionDetailPhotoCopy] [bit] NOT NULL,
	[OriginalBill] [bit] NOT NULL,
	[CashMemo] [bit] NOT NULL,
	[ClaimStatusId] [int] NOT NULL,
	[RejectReason] [nvarchar](200) NULL,
	[PhysicalSubmit] [bit] NOT NULL,
	[ApprovedAmount] [money] NULL,
	[CreatedBy] [nvarchar](30) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [datetime] NULL

PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_MedicalCardPhotoCopy]  DEFAULT ((0)) FOR [MedicalCardPhotoCopy]
GO

ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_PrescriptionDetailPhotoCopy]  DEFAULT ((0)) FOR [PrescriptionDetailPhotoCopy]
GO

ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_OriginalBill]  DEFAULT ((0)) FOR [OriginalBill]
GO

ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_CashMemo]  DEFAULT ((0)) FOR [CashMemo]
GO

ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_ClaimStatusId]  DEFAULT ((1)) FOR [ClaimStatusId]
GO

ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_PhysicalSubmit]  DEFAULT (0) FOR [PhysicalSubmit]
GO


CREATE PROCEDURE [dbo].[SaveMediclaimNonCashless]
(
	@EmployeeNumber nvarchar(15),
	@PPONumber nvarchar(10),
	@MedicalSectionPageNumber int,	
	@MedicalCardHolderName nvarchar(30),
	@Designation nvarchar(30),
	@PatientName nvarchar(30),
	@Gender nvarchar(6),
	@DateOfBirth date,
	@Age int,
	@ClaimFor nvarchar(9),
	@RelationWithRetire nvarchar(8) NULL,
	@MobileNumber nvarchar(10),
	@CardCategory nvarchar(20),
	@Address nvarchar(60),
	@ClaimType nvarchar (3),
	@MedicalCardPhotoCopy bit,
	@PrescriptionDetailPhotoCopy bit ,
	@OriginalBill bit,
	@CashMemo bit,
	@ClaimStatusId int,
	@CreatedBy nvarchar(30) NULL,
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
			,[RelationWithRetire]
			,[MobileNumber]
			,[CardCategory]
			,[Address]
			,[ClaimType]
			,[MedicalCardPhotoCopy]
			,[PrescriptionDetailPhotoCopy]
			,[OriginalBill]
			,[CashMemo]
			,[ClaimStatusId]
			,[CreatedBy])
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
			,@RelationWithRetire
			,@MobileNumber
			,@CardCategory
			,@Address
			,@ClaimType
			,@MedicalCardPhotoCopy
			,@PrescriptionDetailPhotoCopy
			,@OriginalBill
			,@CashMemo
			,@ClaimStatusId
			,@CreatedBy)
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



CREATE PROCEDURE [dbo].[UpdateNonCashlessPhysicalSubmit]
(
		@ClaimId int,
		@PhysicalSubmit bit,	
		@ModifiedBy nvarchar(30) NULL
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[MediclaimNonCashless] 
		SET PhysicalSubmit = @PhysicalSubmit
			,ModifiedBy = @ModifiedBy
			,ModifiedDate = GETDATE()
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



CREATE PROCEDURE [dbo].[GetNonCashlessListByCreatedBy]
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
	  ,[RelationWithRetire]
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
  FROM [dbo].[MediclaimNonCashless]
  WHERE
	[CreatedBy] = @CreatedBy
END
GO


CREATE PROCEDURE dbo.ApproveRejectNonCashlessClaim(
	@ClaimId INT,
	@ClaimStatusId INT,
	@RejectReason NVARCHAR(200) = NULL,
	@ApprovedAmount MONEY,
	@ModifiedBy  NVARCHAR(50)
)
AS
BEGIN TRAN
BEGIN TRY 

	UPDATE dbo.MediclaimNonCashless 
		SET ClaimStatusId = @ClaimStatusId,
		RejectReason = @RejectReason,
		ApprovedAmount =@ApprovedAmount,
		ModifiedBy = @ModifiedBy,
		ModifiedDate = GETUTCDATE()
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

CREATE PROCEDURE [dbo].[GetNonCashlessDetailByClaimId]
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
	  ,[RelationWithRetire]
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
  FROM [dbo].[MediclaimNonCashless]
  WHERE
	[ClaimId] = @ClaimId
END
GO

CREATE PROCEDURE dbo.GetPendingNonCashlessClaims
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
      ,[RelationWithRetire]
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
  FROM [DTLPension].[dbo].[MediclaimNonCashless]
  WHERE ClaimStatusId = 1
END

CREATE PROCEDURE dbo.GetNonCashlessClaimByParam(
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
      ,[RelationWithRetire]
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
  FROM [dbo].[MediclaimNonCashless]
  WHERE 
	(CONVERT(DATE,[CreatedDate],102) = (CONVERT(DATE,@DATE,102)) OR @Date IS NULL)
	AND ((ClaimId = @ClaimId) OR @ClaimId IS NULL)
	AND ((MedicalSectionPageNumber = @PageNumber) OR @PageNumber IS NULL)
END
GO

----------------END----------------


--------------DISPENSARY-------------
----------------START----------------
CREATE TABLE dbo.Dispensary
(
	[DispensaryId] INT PRIMARY KEY IDENTITY(1,1),
	[DispensaryDate] Date NOT NULL,
	[DispensaryLocation] NVARCHAR(50) NOT NULL,
	[DoctorVisited] NVARCHAR(30) NOT NULL,
	[TokenOPDNo] NVARCHAR(30) NOT NULL,
	[MedicineAmount] MONEY NOT NULL,
	[Investigation] NVARCHAR(150) NOT NULL,
	[TotalAmount] MONEY NOT NULL,
	[NonCashlessClaimId] INT NOT NULL,
	[CreatedBy] NVARCHAR(30) NOT NULL,
	[CreatedDate] DATE NOT NULL,
	[ModifiedBy] NVARCHAR(30) NULL,
	[ModifiedDate] DATE NULL
)

ALTER TABLE [dbo].[Dispensary] ADD  CONSTRAINT [DF_Dispensary_CreatdDate]  DEFAULT (GETDATE()) FOR [CreatedDate]
GO
 

CREATE PROCEDURE dbo.SaveDispensary
(	
	@DispensaryDate date,
	@DispensaryLocation nvarchar(50),
	@DoctorVisited nvarchar(30),
	@TokenOPDNo nvarchar(30),
	@MedicineAmount money,
	@Investigation nvarchar(150),
	@TotalAmount money,
	@NonCashlessClaimId int,
	@CreatedBy nvarchar(30)
)
AS
BEGIN TRAN 
BEGIN TRY
	INSERT INTO [dbo].[Dispensary]
	           ([DispensaryDate]
			   ,[DispensaryLocation]
	           ,[DoctorVisited]
	           ,[TokenOPDNo]
	           ,[MedicineAmount]
	           ,[Investigation]
	           ,[TotalAmount]
	           ,[NonCashlessClaimId]
	           ,[CreatedBy])
	     VALUES
	           (@DispensaryDate
			   ,@DispensaryLocation
	           ,@DoctorVisited
	           ,@TokenOPDNo
	           ,@MedicineAmount
	           ,@Investigation
	           ,@TotalAmount
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

CREATE PROCEDURE [dbo].[GetDispensaryByClaimId]
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

---------------------END--------------------


------------------IPD----------------
----------------START----------------
CREATE TABLE dbo.IPD
(
	[IPDId] INT PRIMARY KEY IDENTITY(1,1),
	[DateOfAddmission] Date NOT NULL,
	[HospitalName] NVARCHAR(40) NOT NULL,
	[TreatmentIllsness] NVARCHAR(100) NOT NULL,
	[DateOfDischarge] DATE NOT NULL,
	[TotalAmount] MONEY NOT NULL,
	[NonCashlessClaimId] INT NOT NULL,
	[CreatedBy] NVARCHAR(30) NOT NULL,
	[CreatedDate] DATE NOT NULL,
	[ModifiedBy] NVARCHAR(30) NULL,
	[ModifiedDate] DATE NULL
)

ALTER TABLE [dbo].[IPD] ADD  CONSTRAINT [DF_IPD_CreatdDate]  DEFAULT (GETDATE()) FOR [CreatedDate]
GO


CREATE PROCEDURE dbo.SaveIPD
(	
	@DateOfAddmission date,
	@HospitalName nvarchar(40),
	@TreatmentIllsness nvarchar(150),
	@DateOfDischarge date,
	@TotalAmount money,
	@NonCashlessClaimId int,
	@CreatedBy nvarchar(30)
)
AS
BEGIN TRAN 
BEGIN TRY
	INSERT INTO [dbo].[IPD]
	           ([DateOfAddmission]
	           ,[HospitalName]
	           ,[TreatmentIllsness]
	           ,[DateOfDischarge]
	           ,[TotalAmount]
	           ,[NonCashlessClaimId]
	           ,[CreatedBy])
	     VALUES
	           (@DateOfAddmission
	           ,@HospitalName
	           ,@TreatmentIllsness
	           ,@DateOfDischarge
	           ,@TotalAmount
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


CREATE PROCEDURE [dbo].[GetIPDByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT
	   [IPDId]
      ,[DateOfAddmission]
      ,[HospitalName]
      ,[TreatmentIllsness]
      ,[DateOfDischarge]
      ,[TotalAmount]
      ,[NonCashlessClaimId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[IPD]
  WHERE 
	[NonCashlessClaimId] = @ClaimId
END




----------------END-----------------


------------------OPDCNDPD----------------
------------------START-------------------
CREATE TABLE dbo.OPDCND
(
	[OPDCNDId] INT PRIMARY KEY IDENTITY(1,1),
	[OPDCNDDate] Date NOT NULL,
	[HospitalName] NVARCHAR(40) NOT NULL,
	[MedicineAmount] MONEY NOT NULL,
	[InvestigationAmount] MONEY NOT NULL,
	[ConsultationAmount] MONEY NOT NULL,
	[TotalAmount] MONEY NOT NULL,
	[HospitalizationClaimType] NCHAR(3) NOT NULL,
	[NonCashlessClaimId] INT NOT NULL,
	[CreatedBy] NVARCHAR(30) NOT NULL,
	[CreatedDate] DATE NOT NULL,
	[ModifiedBy] NVARCHAR(30) NULL,
	[ModifiedDate] DATE NULL
)

ALTER TABLE [dbo].[OPDCND] ADD  CONSTRAINT [DF_OPDCND_CreatdDate]  DEFAULT (GETDATE()) FOR [CreatedDate]
GO



CREATE PROCEDURE dbo.SaveOPDCND
(	
	@OPDCNDDate date,
	@HospitalName nvarchar(40),
	@MedicineAmount money,
	@InvestigationAmount money,
	@ConsultationAmount money,
	@TotalAmount money,
	@HospitalizationClaimType nchar(3),
	@NonCashlessClaimId int,
	@CreatedBy nvarchar(30)
)
AS
BEGIN TRAN 
BEGIN TRY
	INSERT INTO [dbo].[OPDCND]
           ([OPDCNDDate]
           ,[HospitalName]
           ,[MedicineAmount]
           ,[InvestigationAmount]
           ,[ConsultationAmount]
           ,[TotalAmount]
           ,[HospitalizationClaimType]
           ,[NonCashlessClaimId]
           ,[CreatedBy])
     VALUES
           (@OPDCNDDate
           ,@HospitalName
           ,@MedicineAmount
           ,@InvestigationAmount 
           ,@ConsultationAmount
           ,@TotalAmount
           ,@HospitalizationClaimType
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


CREATE PROCEDURE [dbo].[GetOPDCNDByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT [OPDCNDId]
      ,[OPDCNDDate]
      ,[HospitalName]
      ,[MedicineAmount]
      ,[InvestigationAmount]
      ,[ConsultationAmount]
      ,[TotalAmount]
      ,[HospitalizationClaimType]
      ,[NonCashlessClaimId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[OPDCND]
  WHERE 
	[NonCashlessClaimId] = @ClaimId
END

----------------END-----------------


--------------GPF-----------------
-------------START----------------
CREATE TABLE dbo.GPFProcessing
(
	GPFProcessingId INT IDENTITY(1,1),
	EmployeeNumber	NVARCHAR(15),
	EmployeeName NVARCHAR(50),
	Employer NVARCHAR(50),
	Designation NVARCHAR(50),
	MemberContribution MONEY,
	MemberInterest DECIMAL,
	GPFAmount MONEY,
	[Month] INT,
	[Year] INT,
	CreatedBy NVARCHAR(50),
	CreatedDate DATE DEFAULT GETDATE(),
	ModifiedBy NVARCHAR(50) NULL,
	ModifiedDate DATE NULL

	CONSTRAINT PK_GPFProcessingId PRIMARY KEY(GPFProcessingId)
)

CREATE PROCEDURE [dbo].[GetGPFReportByParam]
(
	@Employer NVARCHAR(50) NULL,
	@Month INT  = NULL,
	@Year INT = NULL,
	@EmployeeId NVARCHAR(15) = NULL,
	@EmployeeName NVARCHAR(50) = NULL 
)
AS
BEGIN
		SELECT [GPFProcessingId]
		  ,[EmployeeNumber]
		  ,[EmployeeName]
		  ,[Employer]
		  ,[Designation]
		  ,[MemberContribution]
		  ,[MemberInterest]
		  ,[GPFAmount]
		  ,[Month]
		  ,[Year]
		  ,[CreatedBy]
		  ,[CreatedDate]
		  ,[ModifiedBy]
		  ,[ModifiedDate]
	  FROM [dbo].[GPFProcessing]

	WHERE 
	(([Month] = @Month) OR @Month IS NULL)
	AND(([Year]=@Year) OR @Year IS NULL)
	AND ((EmployeeNumber = @EmployeeId) OR @EmployeeId IS NULL)
	AND ((Employer = @Employer) OR @Employer IS NULL)
	AND ((EmployeeName = @EmployeeName) OR @EmployeeName IS NULL)
END


CREATE PROCEDURE dbo.GetGPFProcessingSummaryByParam
(
	@Employer NVARCHAR(50) = NULL,
	@EmployeName NVARCHAR(50) = NULL,
	@EmployeNumber NVARCHAR(15) = NULL
)
AS
BEGIN
	SELECT  SUM(MemberContribution) AS [MemberContribution], SUM(MemberInterest) AS [MemberInterest], [Month], [Year]
	FROM dbo.GPFProcessing 
	WHERE 
		((Employer = @Employer) OR  @Employer IS NULl)
		AND ((EmployeeName = @EmployeName) OR @EmployeName IS NULL)
		AND ((@EmployeNumber = @EmployeNumber) OR @EmployeNumber IS NULL)
	GROUP BY [Month], [Year]

END



CREATE PROCEDURE dbo.SaveGPFProcessing
(
	@xml XML,
	@CreatedBy NVARCHAR(50) 
)
AS
BEGIN TRANSACTION
BEGIN TRY
	 SET NOCOUNT ON;        
 
      INSERT INTO dbo.GPFProcessing
	   ([EmployeeNumber]
		,[EmployeeName]
		,[Employer]
		,[Designation]
		,[MemberContribution]
		,[MemberInterest]
		,[GPFAmount]
		,[Month]
		,[Year]
		,[CreatedBy])
      SELECT
		GPF.value('(EmployeeNumber)[1]','VARCHAR(15)') AS EmployeeNumber, --TAG
		GPF.value('(EmployeeName)[1]','VARCHAR(50)') AS EmployeeName, --TAG
		GPF.value('(Employer)[1]','VARCHAR(50)') AS Employer, --TAG
		GPF.value('(Designation)[1]','VARCHAR(50)') AS Designation, --TAG
		GPF.value('(MemberContribution)[1]','MONEY') AS MemberContribution, --TAG
		GPF.value('(MemberInterest)[1]','DECIMAL') AS MemberInterest, --TAG
		GPF.value('(GPFAmount)[1]','MONEY') AS GPFAmount, --TAG
		GPF.value('(Month)[1]','INT') AS [Month], --TAG
		GPF.value('(Year)[1]','INT') AS [Year], --TAG
      @CreatedBy AS CreatedBy --TAG
	  FROM
      @xml.nodes('/ArrayOfGPFProcessingModel/GPFProcessingModel')AS TEMPTABLE(GPF)
	COMMIT TRANSACTION
END TRY
BEGIN CATCH 

ROLLBACK TRANSACTION
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
               @ErrorState) -- State.
END CATCH

CREATE TABLE [dbo].[GPFWithdraw](
	[WithdrawId] [int] IDENTITY(1,1) NOT NULL,
	[WithdrawType] [nvarchar](13) NOT NULL,
	[AccountHolderName] [nvarchar](50) NOT NULL,
	[EmployeId] [nvarchar](15) NOT NULL,
	[Designation] [nvarchar](20) NOT NULL,
	[Employer] [nvarchar](50) NOT NULL,
	[MonthlyGPFPay] [money] NOT NULL,
	[DateOfJoining] [date] NOT NULL,
	[PurposeOfWithdrawal] [nvarchar](150) NOT NULL,
	[MobileNumber] [nvarchar](10) NOT NULL,
	[DependantsName] [nvarchar](50) NOT NULL,
	[DependentsAge] [float] NOT NULL,
	[DependentsAddress] [nvarchar](150) NOT NULL,
	[TotalGPFContribution] [money] NULL,
	[TotalWithdrawalAmount] [money] NULL,
	[RemainingContribution] [money] NULL,
	[TotalAdvancedAmount] [money] NULL,
	[NoOfEMI] [int] NULL,
	[MonthlyDeduction] [money] NULL,
	[PurposeOfRefundable] [nvarchar](40) NULL,
	[PostingLastThreeYear] [nvarchar](50) NOT NULL,
	[DateOfApplication] [date] NOT NULL,
	[ReasonOfAdvance] [nvarchar](400) NULL,
	[BankAccountNo] [nvarchar](20) NOT NULL,
	[IFSCNo] [nvarchar](30) NOT NULL,
	[BranchName] [nvarchar](50) NOT NULL,
	[BC] [nvarchar](20) NOT NULL,
	[BankName] [nvarchar](50) NOT NULL,
	[ApplicationStatus] [int] NOT NULL,
	[RejectReason] [nvarchar](200) NULL,
	[PhysicalSubmit] [bit] NULL,
	[CreatedBy] [nvarchar](40) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](40) NULL,
	[ModifiedDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_WithdrawId] PRIMARY KEY CLUSTERED 
(
	[WithdrawId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GPFWithdraw] ADD  CONSTRAINT [DF__GPFWithdr__Appli__214BF109]  DEFAULT ((1)) FOR [ApplicationStatus]
GO

ALTER TABLE [dbo].[GPFWithdraw] ADD  CONSTRAINT [DF__GPFWithdr__Creat__22401542]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[GPFWithdraw] ADD  CONSTRAINT [DF_GPFWithdraw_Active]  DEFAULT ((1)) FOR [Active]
GO


CREATE PROCEDURE dbo.SaveGPFWithdraw
(
	@WithdrawType NVARCHAR(13),
	@AccountHolderName NVARCHAR(50),
	@EmployeId NVARCHAR(15),
	@Designation NVARCHAR(20),
	@Employer NVARCHAR(50),
	@MonthlyGPFPay MONEY,
	@DateOfJoining DATE,
	@PurposeOfWithdrawal NVARCHAR(150),
	@MobileNumber NVARCHAR(10),
	@DependantsName NVARCHAR(50),
	@DependentsAge FLOAT,
	@DependentsAddress NVARCHAR(150),
	--Non Refundable--
	@TotalGPFContribution MONEY NULL,
	@TotalWithdrawalAmount MONEY NULL,
	@RemainingContribution MONEY NULL,
	--Refundable--
	@TotalAdvancedAmount MONEY NULL,
	@NoOfEMI INT NULL,
	@MonthlyDeduction MONEY NULL,
	@PurposeOfRefundable NVARCHAR(40),
	
	@PostingLastThreeYear NVARCHAR(50),
	@DateOfApplication DATE,
	@ReasonOfAdvance NVARCHAR(400) NULL,
	
	@BankAccountNo NVARCHAR(20),
	@IFSCNo NVARCHAR(30),
	@BranchName NVARCHAR(50),
	@BC NVARCHAR(20),
	@BankName NVARCHAR(50),
	
	@CreatedBy NVARCHAR(40),

	@WithdrawId  INT OUTPUT
)
AS
BEGIN
BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO dbo.GPFWithdraw
		(
			WithdrawType
			,AccountHolderName
			,EmployeId
			,Designation
			,Employer
			,MonthlyGPFPay
			,DateOfJoining
			,PurposeOfWithdrawal
			,MobileNumber
			,DependantsName
			,DependentsAge
			,DependentsAddress
			--Non Refundable--
			,TotalGPFContribution
			,TotalWithdrawalAmount
			,RemainingContribution
			--Refundable--
			,TotalAdvancedAmount
			,NoOfEMI
			,MonthlyDeduction
			,PurposeOfRefundable
			--
			,PostingLastThreeYear
			,DateOfApplication
			,ReasonOfAdvance
			--
			,BankAccountNo
			,IFSCNo
			,BranchName
			,BC
			,BankName
			--
			,CreatedBy
		)
		VALUES
		(
			@WithdrawType
			,@AccountHolderName
			,@EmployeId
			,@Designation
			,@Employer
			,@MonthlyGPFPay
			,@DateOfJoining
			,@PurposeOfWithdrawal
			,@MobileNumber
			,@DependantsName
			,@DependentsAge
			,@DependentsAddress
			--Non Refundable--
			,@TotalGPFContribution
			,@TotalWithdrawalAmount
			,@RemainingContribution
			--Refundable--
			,@TotalAdvancedAmount
			,@NoOfEMI
			,@MonthlyDeduction
			,@PurposeOfRefundable
			--
			,@PostingLastThreeYear
			,@DateOfApplication
			,@ReasonOfAdvance
			--
			,@BankAccountNo
			,@IFSCNo
			,@BranchName
			,@BC
			,@BankName
			--
			,@CreatedBy
		)
		COMMIT TRANSACTION
		SET @WithdrawId = SCOPE_IDENTITY();
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
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
END


CREATE PROCEDURE dbo.DeleteGPFWithdraw
(
	@WithdrawId INT,
	@ModifiedBY NVARCHAR(50)
)
AS
BEGIN
BEGIN TRANSACTION
	BEGIN TRY
		UPDATE dbo.GPFWithdraw SET 
			ModifiedBy = @ModifiedBy
			,ModifiedDate = GETDATE()
			,Active = 0
		WHERE
			WithdrawId = @WithdrawId 
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
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
END


CREATE PROCEDURE dbo.GetGPFWithdrawalByParam  
(  
 @WithdrawId  INT = NULL,  
 @WithdrawType NVARCHAR(13) = NULL,  
 @AccountHolderName NVARCHAR(50) = NULL,  
 @EmployeId NVARCHAR(15) = NULL,  
 @Employer NVARCHAR(50) = NULL,  
 @DateOfJoining DATE = NULL,  
 @DateOfApplication DATE = NULL,  
 @Month INT = NULL,  
 @Year INT = NULL,  
 @ApplicationStatusId INT = NULL,  
 @CreatedBy NVARCHAR(40) = NULL,
 @Active BIT = 1
)  
AS  
BEGIN  
  
  SELECT     
   WithdrawId  
   ,WithdrawType  
   ,AccountHolderName  
   ,EmployeId  
   ,Designation  
   ,Employer  
   ,MonthlyGPFPay  
   ,DateOfJoining  
   ,PurposeOfWithdrawal  
   ,MobileNumber  
   ,DependantsName  
   ,DependentsAge  
   ,DependentsAddress  
   --Non Refundable--  
   ,TotalGPFContribution  
   ,TotalWithdrawalAmount  
   ,RemainingContribution  
   --Refundable--  
   ,TotalAdvancedAmount  
   ,NoOfEMI  
   ,MonthlyDeduction  
   ,PurposeOfRefundable  
   --  
   ,PostingLastThreeYear  
   ,DateOfApplication  
   ,ReasonOfAdvance  
   --  
   ,BankAccountNo  
   ,IFSCNo  
   ,BranchName  
   ,BC  
   ,BankName   
   ,ApplicationStatus
   ,PhysicalSubmit
   ,RejectReason
   ,CreatedBy  
   ,ModifiedBy  
   ,ModifiedDate  
  FROM dbo.GPFWithdraw  
  WHERE   
   ((WithdrawId = @WithdrawId) OR @WithdrawId IS NULL)  
   AND ((WithdrawType = @WithdrawType) OR @WithdrawType IS NULL)  
   AND ((AccountHolderName = @AccountHolderName) OR @AccountHolderName IS NULL)  
   AND ((EmployeId = @EmployeId) OR @EmployeId IS NULL)  
   AND ((Employer = @Employer) OR @Employer IS NULL)  
   AND (CONVERT(DATE,[DateOfJoining],102) = (CONVERT(DATE,@DateOfJoining,102)) OR @DateOfJoining IS NULL)  
   AND (CONVERT(DATE,DateOfApplication,102) = CONVERT(DATE,@DateOfApplication,102) OR @DateOfApplication IS NULL)  
   AND ((Month(DateOfApplication) = @Month) OR @Month IS NULL)  
   AND ((Year(DateOfApplication)=@Year) OR @Year IS NULL)  
   AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)  
   AND ((ApplicationStatus = @ApplicationStatusId) OR @ApplicationStatusId IS NULL)  
   AND ((Active = @Active) OR @Active IS NULL)
END  
  
GO



CREATE PROCEDURE [dbo].[ApproveRejectGPFWithdrawal](
	@WithdrawId INT,
	@ApplicationStatusId INT,
	@RejectReason NVARCHAR(200) = NULL,
	@ModifiedBy  NVARCHAR(50)
)
AS
BEGIN TRAN
BEGIN TRY 

	UPDATE dbo.GPFWithdraw 
		SET ApplicationStatus = @ApplicationStatusId,
		RejectReason = @RejectReason,
		ModifiedBy = @ModifiedBy,
		ModifiedDate = GETUTCDATE()
	WHERE
		WithdrawId = @WithdrawId
	
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



CREATE PROCEDURE [dbo].[UpdateGPFWithdrawalPhysicalSubmit]
(
		@WithdrawId int,
		@PhysicalSubmit bit,	
		@ModifiedBy nvarchar(30) NULL
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[GPFWithdraw] 
		SET PhysicalSubmit = @PhysicalSubmit
			,ModifiedBy = @ModifiedBy
			,ModifiedDate = GETDATE()
		WHERE 
			WithdrawId = @WithdrawId
      
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


CREATE PROCEDURE [dbo].[UpdateGPFWithdrawal]
(
	@WithdrawId  INT,
	@WithdrawType NVARCHAR(13),
	@AccountHolderName NVARCHAR(50),
	@EmployeId NVARCHAR(15),
	@Designation NVARCHAR(20),
	@Employer NVARCHAR(50),
	@MonthlyGPFPay MONEY,
	@DateOfJoining DATE,
	@PurposeOfWithdrawal NVARCHAR(150),
	@MobileNumber NVARCHAR(10),
	@DependantsName NVARCHAR(50),
	@DependentsAge FLOAT,
	@DependentsAddress NVARCHAR(150),
	--Non Refundable--
	@TotalGPFContribution MONEY NULL,
	@TotalWithdrawalAmount MONEY NULL,
	@RemainingContribution MONEY NULL,
	--Refundable--
	@TotalAdvancedAmount MONEY NULL,
	@NoOfEMI INT NULL,
	@MonthlyDeduction MONEY NULL,
	@PurposeOfRefundable NVARCHAR(40),
	
	@PostingLastThreeYear NVARCHAR(50),
	@DateOfApplication DATE,
	@ReasonOfAdvance NVARCHAR(400) NULL,
	@ApplicationStatus INT,
	
	@BankAccountNo NVARCHAR(20),
	@IFSCNo NVARCHAR(30),
	@BranchName NVARCHAR(50),
	@BC NVARCHAR(20),
	@BankName NVARCHAR(50),
	
	@ModifiedBy NVARCHAR(40)
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[GPFWithdraw] 
		SET --PhysicalSubmit = @PhysicalSubmit
			 WithdrawType  = @WithdrawType
			,AccountHolderName = @AccountHolderName
			,EmployeId = @EmployeId
			,Designation = @Designation
			,Employer = @Employer
			,MonthlyGPFPay = @MonthlyGPFPay
			,DateOfJoining = @DateOfJoining
			,PurposeOfWithdrawal = @PurposeOfWithdrawal
			,MobileNumber = @MobileNumber
			,DependantsName = @DependantsName
			,DependentsAge = @DependentsAge
			,DependentsAddress = @DependentsAddress
			--Non Refundable--
			,TotalGPFContribution = @TotalGPFContribution
			,TotalWithdrawalAmount = @TotalWithdrawalAmount
			,RemainingContribution = @RemainingContribution
			--Refundable--
			,TotalAdvancedAmount = @TotalAdvancedAmount
			,NoOfEMI = @NoOfEMI
			,MonthlyDeduction = @MonthlyDeduction
			,PurposeOfRefundable = @PurposeOfRefundable
			
			,PostingLastThreeYear = @PostingLastThreeYear
			,DateOfApplication = @DateOfApplication
			,ReasonOfAdvance = @ReasonOfAdvance
			,ApplicationStatus = @ApplicationStatus
			,RejectReason = NULL
			
			,BankAccountNo = @BankAccountNo
			,IFSCNo = @IFSCNo
			,BranchName = @BranchName
			,BC = @BC
			,BankName = @BankName
			,ModifiedBy = @ModifiedBy
			,ModifiedDate = GETDATE()
		WHERE 
			WithdrawId = @WithdrawId
      
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


--------------GPF----------------
--------------END----------------

--------------DOCUMENT----------------
--------------START----------------

CREATE TABLE dbo.Document
(
	DocumentId INT IDENTITY(1,1),
	DocumentPath NVARCHAR(200) NOT NULL,
	ApplicationArea NVARCHAR(20) NULL,
	ApplicationSubArea NVARCHAR(20) NULL,
	ReferenceId INT NOT NULL,
	DocumentFor NVARCHAR(40) NULL,
	CreatedBy NVARCHAR(50) NOT NULL,
	CreatedDate DATE NOT NULL DEFAULT GETDATE(),
	ModifiedBy NVARCHAR(20) NULL,
	ModifiedDate DATE NULL
	
	CONSTRAINT PK_DocumentId PRIMARY KEY (DocumentId)
)

ALTER TABLE dbo.Document   ADD Active BIT NOT NULL  CONSTRAINT DF_Active DEFAULT (1);

GO

CREATE PROCEDURE dbo.SaveDocument
(
	@DocumentPath NVARCHAR(200),
	@ApplicationArea NVARCHAR(20),
	@ApplicationSubArea NVARCHAR(20),
	@ReferenceId INT,
	@DocumentFor NVARCHAR(40) = NULL,
	@CreatedBy NVARCHAR(50),
	
	@DocumentId  INT OUTPUT
)
AS
BEGIN
BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO dbo.Document
		(
			DocumentPath
			,ApplicationArea
			,ApplicationSubArea
			,ReferenceId
			,DocumentFor
			,CreatedBy
		)
		VALUES
		(
			@DocumentPath
			,@ApplicationArea
			,@ApplicationSubArea
			,@ReferenceId
			,@DocumentFor
			,@CreatedBy
		)
		COMMIT TRANSACTION
		SET @DocumentId = SCOPE_IDENTITY();
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
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
END

GO

CREATE PROCEDURE dbo.GetDocumentsByParam
(
	@DocumentId  INT = NULL,
	@ReferenceId INT = NULL,
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
			,CreatedBy
			,CreatedDate
			,ModifiedBy
			,ModifiedDate
		FROM dbo.Document
		WHERE 
			((DocumentId = @DocumentId) OR @DocumentId IS NULL)
			AND ((ReferenceId = @ReferenceId) OR @ReferenceId IS NULL)
			AND Active = @Active
			
END

CREATE PROCEDURE [dbo].[UpdateDocument]
(
	@DocumentId  INT,
	@Active BIT,
	@ModifiedBy NVARCHAR(50)	
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[Document] 
		SET Active = @Active
		,ModifiedBy = @ModifiedBy
		,ModifiedDate = GETDATE()
		WHERE 
			DocumentId = @DocumentId

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

--------------DOCUMENT----------------
--------------END----------------



--------------Medical Page Detail----------------
----------------------Start----------------------

CREATE TABLE dbo.MedicalPageDetail
(
	PageDetailId INT IDENTITY(1,1),
	PageNumber INT NOT NULL,
	EmployeeNumber NVARCHAR(15) NOT NULL,
	Name NVARCHAR(30) NOT NULL,
	PPONumber NVARCHAR(30) NOT NULL,
	Designation NVARCHAR(50) NOT NULL,
	Department NVARCHAR(30) NOT NULL,
	CardCategory NVARCHAR(13) NOT NULL,
	MobileNumber NVARCHAR(10) NOT NULL,
	DateOfRetirement DATE NOT NULL,
	SpouseName NVARCHAR(50) NULL,
	LBP NVARCHAR(30) NULL,
	Dispensary NVARCHAR(30) NULL,
	CreatedBy NVARCHAR(50) NOT NULL,
	CreatedDate DATE NOT NULL DEFAULT GETDATE(),
	ModifiedBy NVARCHAR(50) NULL,
	ModifiedDate NVARCHAR(50) NULL,
	CONSTRAINT PK_PageDetailId PRIMARY KEY (PageDetailId)
)


CREATE PROCEDURE dbo.SaveMedicalPageDetail
(
	@PageNumber INT,
	@EmployeeNumber NVARCHAR(15),
	@Name NVARCHAR(30),
	@PPONumber NVARCHAR(30),
	@Designation NVARCHAR(50),
	@Department NVARCHAR(30),
	@CardCategory NVARCHAR(13),
	@MobileNumber NVARCHAR(10),
	@DateOfRetirement DATE,
	@SpouseName NVARCHAR(50),
	@LBP NVARCHAR(30),
	@Dispensary NVARCHAR(30),
	@CreatedBy NVARCHAR(50),
	@PageDetailId INT OUTPUT
)
AS
BEGIN TRANSACTION
BEGIN TRY
	INSERT INTO dbo.MedicalPageDetail
		(
		 PageNumber,
		 EmployeeNumber,
		 Name,
		 PPONumber,
		 Designation,
		 Department,
		 CardCategory,
		 MobileNumber,
		 DateOfRetirement,
		 SpouseName,
		 LBP,
		 Dispensary,
		 CreatedBy)
	VALUES
		 (
			@PageNumber,
			@EmployeeNumber,
			@Name,
			@PPONumber,
			@Designation,
			@Department,
			@CardCategory,
			@MobileNumber,
			@DateOfRetirement,
			@SpouseName,
			@LBP,
			@Dispensary,
			@CreatedBy)
	COMMIT TRANSACTION
	
	 SET @PageDetailId = SCOPE_IDENTITY();
	
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
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




	CREATE PROCEDURE dbo.GetMedicalPageDetailByParam
	(
		@EmployeeNumber NVARCHAR(15) = NULL,
		@PPONumber NVARCHAR(30) = NULL,
		@Department NVARCHAR(30) = NULL,
		@PageNumber INT = NULL,
		@CreatedBy NVARCHAR(40) = NULL
	)
	AS
	BEGIN
		SELECT
			 PageDetailId,
			 PageNumber,
			 EmployeeNumber,
			 Name,
			 PPONumber,
			 Designation,
			 Department,
			 CardCategory,
			 MobileNumber,
			 DateOfRetirement,
			 SpouseName,
			 LBP,
			 Dispensary,
			 CreatedBy,
			 CreatedDate,
			 ModifiedBy,
			 ModifiedDate
	
		FROM dbo.MedicalPageDetail
		WHERE
			((PageNumber = @PageNumber) OR @PageNumber IS NULL)
			AND ((EmployeeNumber LIKE '%'+@EmployeeNumber+'%') OR @EmployeeNumber IS NULL)
			AND ((PPONumber LIKE '%'+@PPONumber+'%') OR @PPONumber IS NULL)
			AND ((Department LIKE '%'+@Department+'%') OR @Department IS NULL)
			AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)
		
	
	END
--------------Medical Page Detail----------------
-----------------------End-----------------------

-----------------ALTER Queries----------------
-------------------Start----------------------
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  Active BIT 
GO
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_Active]  DEFAULT ((1)) FOR [Active]
GO