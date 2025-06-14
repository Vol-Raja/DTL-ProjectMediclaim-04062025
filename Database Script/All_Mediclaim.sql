USE [DTLPension]
GO
/****** Object:  Table [dbo].[ClaimStatusMaster]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClaimStatusMaster](
	[Id] [int] NOT NULL,
	[Status] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClaimTypeMaster]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClaimTypeMaster](
	[Id] [int] NOT NULL,
	[Type] [nvarchar](11) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dispensary]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dispensary](
	[DispensaryId] [int] IDENTITY(1,1) NOT NULL,
	[DispensaryDate] [date] NOT NULL,
	[DispensaryLocation] [nvarchar](50) NOT NULL,
	[DoctorVisited] [nvarchar](30) NOT NULL,
	[TokenOPDNo] [nvarchar](30) NOT NULL,
	[MedicineAmount] [money] NOT NULL,
	[Investigation] [nvarchar](150) NOT NULL,
	[TotalAmount] [money] NOT NULL,
	[NonCashlessClaimId] [int] NOT NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[DispensaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IPD]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IPD](
	[IPDId] [int] IDENTITY(1,1) NOT NULL,
	[DateOfAddmission] [date] NOT NULL,
	[HospitalName] [nvarchar](40) NOT NULL,
	[TreatmentIllsness] [nvarchar](100) NOT NULL,
	[DateOfDischarge] [date] NOT NULL,
	[TotalAmount] [money] NOT NULL,
	[NonCashlessClaimId] [int] NOT NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[IPDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalCard]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalCard](
	[MedicalCardId] [int] IDENTITY(1,1) NOT NULL,
	[CardNo] [nvarchar](30) NOT NULL,
	[EmployeeNo] [nvarchar](30) NOT NULL,
	[PPONo] [nvarchar](30) NOT NULL,
	[NameOfCardHolder] [nvarchar](30) NOT NULL,
	[Employer] [nvarchar](30) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [nvarchar](6) NOT NULL,
	[MedicalSectionPageNo] [int] NOT NULL,
	[CardCategory] [nvarchar](13) NOT NULL,
	[MobileNumber] [nvarchar](10) NOT NULL,
	[Address] [nvarchar](150) NOT NULL,
	[MedicalHistory] [nvarchar](150) NOT NULL,
	[BankName] [nvarchar](50) NOT NULL,
	[BICCode] [nvarchar](15) NOT NULL,
	[IFSCCode] [nvarchar](20) NOT NULL,
	[AccountNumber] [nvarchar](100) NOT NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_MedicalCardId] PRIMARY KEY CLUSTERED 
(
	[MedicalCardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalPageDetail]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalPageDetail](
	[PageDetailId] [int] IDENTITY(1,1) NOT NULL,
	[PageNumber] [int] NOT NULL,
	[EmployeeNumber] [nvarchar](15) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[PPONumber] [nvarchar](30) NOT NULL,
	[Designation] [nvarchar](50) NOT NULL,
	[Department] [nvarchar](30) NOT NULL,
	[CardCategory] [nvarchar](13) NOT NULL,
	[MobileNumber] [nvarchar](10) NOT NULL,
	[DateOfRetirement] [date] NOT NULL,
	[SpouseName] [nvarchar](50) NULL,
	[LBP] [nvarchar](30) NULL,
	[Dispensary] [nvarchar](30) NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_PageDetailId] PRIMARY KEY CLUSTERED 
(
	[PageDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediclaimCashless]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MediclaimCashless](
	[ClaimId] [int] IDENTITY(1,1) NOT NULL,
	[NameOfHospital] [nvarchar](100) NOT NULL,
	[HospitalPhoneNumber] [nvarchar](16) NOT NULL,
	[HospitalAddress] [nvarchar](250) NOT NULL,
	[HospitalId] [nvarchar](15) NOT NULL,
	[EmailId] [nvarchar](50) NOT NULL,
	[NameOfPatient] [nvarchar](30) NOT NULL,
	[Gender] [nvarchar](6) NOT NULL,
	[PatientPhoneNumber] [nvarchar](16) NOT NULL,
	[PatientAddress] [nvarchar](250) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Age] [int] NOT NULL,
	[MedicalSectionPageNumber] [int] NOT NULL,
	[NameOfCardHolder] [varchar](80) NOT NULL,
	[MedicalCardNumber] [nvarchar](16) NOT NULL,
	[AdmissionNumber] [int] NOT NULL,
	[CardCategory] [nvarchar](20) NOT NULL,
	[CaseType] [nvarchar](50) NOT NULL,
	[TypeOfTreatment] [nvarchar](50) NOT NULL,
	[Amount] [money] NOT NULL,
	[DateOfAdmission] [date] NOT NULL,
	[DateOfDischargeOrDeath] [date] NOT NULL,
	[MedicalCard] [bit] NOT NULL,
	[DoctorPrescription] [bit] NOT NULL,
	[OriginalBill] [bit] NOT NULL,
	[BillCashMemo] [bit] NOT NULL,
	[ClaimStatusId] [int] NOT NULL,
	[RejectReason] [nvarchar](200) NULL,
	[PhysicalSubmit] [bit] NOT NULL,
	[ApprovedAmount] [money] NULL,
	[CreatedBy] [nvarchar](30) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MediclaimCashless] PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediclaimNonCashless]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediclaimVoucher]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MediclaimVoucher](
	[VoucherId] [int] IDENTITY(1,1) NOT NULL,
	[PayTo] [nvarchar](30) NOT NULL,
	[ApprovedAmount] [money] NOT NULL,
	[AmountInWords] [nvarchar](100) NULL,
	[EntryNo] [int] NOT NULL,
	[PageNo] [int] NOT NULL,
	[BankBranch] [nvarchar](100) NULL,
	[AccountNumber] [nvarchar](100) NULL,
	[BICCode] [nvarchar](15) NULL,
	[IFSCCode] [nvarchar](10) NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_VoucherId] PRIMARY KEY CLUSTERED 
(
	[VoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OPDCND]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OPDCND](
	[OPDCNDId] [int] IDENTITY(1,1) NOT NULL,
	[OPDCNDDate] [date] NOT NULL,
	[HospitalName] [nvarchar](40) NOT NULL,
	[MedicineAmount] [money] NOT NULL,
	[InvestigationAmount] [money] NOT NULL,
	[ConsultationAmount] [money] NOT NULL,
	[TotalAmount] [money] NOT NULL,
	[HospitalizationClaimType] [nchar](3) NOT NULL,
	[NonCashlessClaimId] [int] NOT NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[OPDCNDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Dispensary] ADD  CONSTRAINT [DF_Dispensary_CreatdDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[IPD] ADD  CONSTRAINT [DF_IPD_CreatdDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MedicalCard] ADD  CONSTRAINT [DF__MedicalCa__Creat__6CA31EA0]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MedicalPageDetail] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_MedicalCard]  DEFAULT ((0)) FOR [MedicalCard]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_DoctorPrescription]  DEFAULT ((0)) FOR [DoctorPrescription]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_Bills]  DEFAULT ((0)) FOR [OriginalBill]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_PhotoId]  DEFAULT ((0)) FOR [BillCashMemo]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_ClaimStatusId]  DEFAULT ((1)) FOR [ClaimStatusId]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_PhysicalSubmit]  DEFAULT ((0)) FOR [PhysicalSubmit]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
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
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_PhysicalSubmit]  DEFAULT ((0)) FOR [PhysicalSubmit]
GO
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MediclaimVoucher] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[OPDCND] ADD  CONSTRAINT [DF_OPDCND_CreatdDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
/****** Object:  StoredProcedure [dbo].[ApproveRejectCashlessClaim]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ApproveRejectCashlessClaim](
	@ClaimId INT,
	@ClaimStatusId INT,
	@RejectReason NVARCHAR(200) = NULL,
	@ApprovedAmount MONEY,
	@ModifiedBy  NVARCHAR(50)
)
AS
BEGIN TRAN
BEGIN TRY 

	UPDATE dbo.MediclaimCashless 
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
/****** Object:  StoredProcedure [dbo].[ApproveRejectNonCashlessClaim]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ApproveRejectNonCashlessClaim](
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
/****** Object:  StoredProcedure [dbo].[GetCashlessClaimByParam]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCashlessClaimByParam](
	@Date Date = NULL,
	@ClaimId INT = NULL,
	@PageNumber INT = NULL 
)
AS
BEGIN
	SELECT [ClaimId],
	[NameOfHospital],
	[HospitalPhoneNumber],
	[HospitalAddress],
	[HospitalId],
	[EmailId],
	[NameOfPatient],
	[Gender],
	[PatientPhoneNumber],
	[PatientAddress],
	[DateOfBirth],
	[Age],
	[MedicalSectionPageNumber],
	[NameOfCardHolder],
	[MedicalCardNumber],
	[AdmissionNumber],
	[CardCategory],
	[CaseType],
	[TypeOfTreatment],
	[Amount],
	[DateOfAdmission],
	[DateOfDischargeOrDeath],
	[MedicalCard],
	[DoctorPrescription],
	[OriginalBill],
	[BillCashMemo],
	[ClaimStatusId],
	[RejectReason],
	[PhysicalSubmit],
	[ApprovedAmount],
	[CreatedBy],
	[CreatedDate],
	[ModifiedBy],
	[ModifiedDate]
  FROM [dbo].[MediclaimCashless]
  WHERE 
	(CONVERT(DATE,[CreatedDate],102) = (CONVERT(DATE,@DATE,102)) OR @Date IS NULL)
	AND ((ClaimId = @ClaimId) OR @ClaimId IS NULL)
	AND ((MedicalSectionPageNumber = @PageNumber) OR @PageNumber IS NULL)
END
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessListByClaimId]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCashlessListByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT [ClaimId],
	[NameOfHospital],
	[HospitalPhoneNumber],
	[HospitalAddress],
	[HospitalId],
	[EmailId],
	[NameOfPatient],
	[Gender],
	[PatientPhoneNumber],
	[PatientAddress],
	[DateOfBirth],
	[Age],
	[MedicalSectionPageNumber],
	[NameOfCardHolder],
	[MedicalCardNumber],
	[AdmissionNumber],
	[CardCategory],
	[CaseType],
	[TypeOfTreatment],
	[Amount],
	[DateOfAdmission],
	[DateOfDischargeOrDeath],
	[MedicalCard],
	[DoctorPrescription],
	[OriginalBill],
	[BillCashMemo],
	[ClaimStatusId],
	[RejectReason],
	[PhysicalSubmit],
	[ApprovedAmount],
	[CreatedBy],
	[CreatedDate],
	[ModifiedBy],
	[ModifiedDate]
  FROM [dbo].[MediclaimCashless]
  WHERE
	[CLaimId] = @ClaimId
END
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessListByCreatedBy]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCashlessListByCreatedBy]
(
	@CreatedBy nvarchar(30)
)
AS
BEGIN
SELECT [CLaimID]
      ,[NameOfHospital]
      ,[HospitalPhoneNumber]
      ,[HospitalAddress]
      ,[HospitalId]
      ,[EmailId]
      ,[NameOfPatient]
      ,[Gender]
      ,[PatientPhoneNumber]
      ,[PatientAddress]
      ,[MedicalSectionPageNumber]
      ,[MedicalCardNumber]
      ,[AdmissionNumber]
      ,[CardCategory]
      ,[CaseType]
      ,[TypeOfTreatment]
      ,[DateOfAdmission]
      ,[DateOfDischargeOrDeath]
      ,[MedicalCard]
      ,[DoctorPrescription]
      ,[OriginalBill]
      ,[BillCashMemo]
	  ,[ClaimStatusId]
	  ,[RejectReason]
	  ,[PhysicalSubmit]
	  ,[ApprovedAmount]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[MediclaimCashless]
  WHERE
	[CreatedBy] = @CreatedBy
END
GO
/****** Object:  StoredProcedure [dbo].[GetDispensaryByClaimId]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[GetIPDByClaimId]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
GO
/****** Object:  StoredProcedure [dbo].[GetMedicalCardByParam]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetMedicalCardByParam]
(
	@MedicalCardId [INT] = NULL,
	@CardNo [NVARCHAR](30) = NULL,
	@EmployeeNo [NVARCHAR](30) = NULL,
	@PPONo [NVARCHAR](30) = NULL,
	@CreatedBy [NVARCHAR](30) = NULL
)
AS
BEGIN
	SELECT
	
		MedicalCardId,
		CardNo,
		EmployeeNo,
		PPONo,
		NameOfCardHolder,
		Employer,
		DateOfBirth,
		Age,
		Gender,
		MedicalSectionPageNo,
		CardCategory,
		MobileNumber,
		[Address],
		MedicalHistory,
		BankName,
		BICCode,
		IFSCCode,
		AccountNumber,
		CreatedBy,
		ModifiedBy
	
	FROM dbo.MedicalCard
	WHERE
	
		((MedicalCardId = @MedicalCardId) OR @MedicalCardId IS NULL)
		AND ((CardNo = @CardNo) OR @CardNo IS NULL)
		AND ((EmployeeNo = @EmployeeNo) OR @EmployeeNo IS NULL)
		AND ((PPONo = @PPONo) OR @PPONo IS NULL)
		AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetMedicalPageDetailByParam]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMedicalPageDetailByParam]
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
GO
/****** Object:  StoredProcedure [dbo].[GetMediclaimVoucherByParam]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMediclaimVoucherByParam]
(
	@VoucherId [INT] = NULL,
	@PayTo [NVARCHAR](30) = NULL,
	@EntryNo [INT] =NULL,
	@PageNo [INT] = NULL,
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
		IFSCCode	
	
	FROM dbo.MediclaimVoucher
	WHERE
		((VoucherId = @VoucherId) OR @VoucherId IS NULL)
		AND ((PayTo LIKE '%'+@PayTo+'%') OR @PayTo IS NULL)
		AND ((EntryNo = @EntryNo) OR @EntryNo IS NULL)
		AND ((PageNo = @PageNo) OR @PageNo IS NULL)
		AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)
	
END

GO
/****** Object:  StoredProcedure [dbo].[GetNonCashlessClaimByParam]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNonCashlessClaimByParam](
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
/****** Object:  StoredProcedure [dbo].[GetNonCashlessDetailByClaimId]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[GetNonCashlessListByCreatedBy]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[GetOPDCNDByClaimId]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
GO
/****** Object:  StoredProcedure [dbo].[GetPendingCashlessClaims]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPendingCashlessClaims]
AS
BEGIN
	SELECT [ClaimId],
	[NameOfHospital],
	[HospitalPhoneNumber],
	[HospitalAddress],
	[HospitalId],
	[EmailId],
	[NameOfPatient],
	[Gender],
	[PatientPhoneNumber],
	[PatientAddress],
	[DateOfBirth],
	[Age],
	[MedicalSectionPageNumber],
	[NameOfCardHolder],
	[MedicalCardNumber],
	[AdmissionNumber],
	[CardCategory],
	[CaseType],
	[TypeOfTreatment],
	[Amount],
	[DateOfAdmission],
	[DateOfDischargeOrDeath],
	[MedicalCard],
	[DoctorPrescription],
	[OriginalBill],
	[BillCashMemo],
	[ClaimStatusId],
	[RejectReason],
	[PhysicalSubmit],
	[ApprovedAmount],
	[CreatedBy],
	[CreatedDate],
	[ModifiedBy],
	[ModifiedDate]
  FROM [DTLPension].[dbo].[MediclaimCashless]
  WHERE ClaimStatusId = 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetPendingNonCashlessClaims]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPendingNonCashlessClaims]
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
GO
/****** Object:  StoredProcedure [dbo].[SaveDispensary]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveDispensary]
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
/****** Object:  StoredProcedure [dbo].[SaveIPD]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveIPD]
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
/****** Object:  StoredProcedure [dbo].[SaveMedicalCard]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveMedicalCard]
(
	@CardNo [NVARCHAR](30),
	@EmployeeNo [NVARCHAR](30),
	@PPONo [NVARCHAR](30),
	@NameOfCardHolder [NVARCHAR](30),
	@Employer [NVARCHAR](30),
	@DateOfBirth [DATE],
	@Age [INT],
	@Gender [NVARCHAR](6),
	@MedicalSectionPageNo [INT],
	@CardCategory [NVARCHAR](13),
	@MobileNumber [NVARCHAR](10),
	@Address [NVARCHAR](150),
	@MedicalHistory [NVARCHAR](150),
	@BankName [NVARCHAR](50),
	@BICCode [NVARCHAR](15),
	@IFSCCode [NVARCHAR](20),
	@AccountNumber[NVARCHAR](100),
	@CreatedBy [NVARCHAR](30),
	@MedicalCardId [INT] OUTPUT
)
AS
BEGIN TRAN
BEGIN TRY
	INSERT INTO dbo.MedicalCard
	(
		CardNo,
		EmployeeNo,
		PPONo,
		NameOfCardHolder,
		Employer,
		DateOfBirth,
		Age,
		Gender,
		MedicalSectionPageNo,
		CardCategory,
		MobileNumber,
		[Address],
		MedicalHistory,
		BankName,
		BICCode,
		IFSCCode,
		AccountNumber,
		CreatedBy)
	 VALUES(
		@CardNo,
		@EmployeeNo,
		@PPONo,
		@NameOfCardHolder,
		@Employer,
		@DateOfBirth,
		@Age,
		@Gender,
		@MedicalSectionPageNo,
		@CardCategory,
		@MobileNumber,
		@Address,
		@MedicalHistory,
		@BankName,
		@BICCode,
		@IFSCCode,
		@AccountNumber,
		@CreatedBy)
	 COMMIT TRAN
	 SET @MedicalCardId = SCOPE_IDENTITY();
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
/****** Object:  StoredProcedure [dbo].[SaveMedicalPageDetail]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveMedicalPageDetail]
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
GO
/****** Object:  StoredProcedure [dbo].[SaveMediclaimCashless]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveMediclaimCashless]
(
	 @NameOfHospital nvarchar(100)
	,@HospitalPhoneNumber nvarchar(16) 
	,@HospitalAddress nvarchar(250)
	,@HospitalId nvarchar(15)
	,@EmailId nvarchar(50)
	,@NameOfPatient nvarchar(30)
	,@Gender nvarchar(6)
	,@PatientPhoneNumber nvarchar(16)
	,@PatientAddress nvarchar(250)
	,@DateOfBirth date
	,@Age int
	,@MedicalSectionPageNumber int
	,@NameOfCardHolder nvarchar(80)
	,@MedicalCardNumber nvarchar(16)
	,@AdmissionNumber int
	,@CardCategory nvarchar(20)
	,@CaseType nvarchar(50)
	,@TypeOfTreatment nvarchar(50)
	,@Amount money
	,@DateOfAdmission date
	,@DateOfDischargeOrDeath date
	,@MedicalCard bit
	,@DoctorPrescription bit
	,@OriginalBill bit
	,@BillCashMemo bit
	,@CreatedBy nvarchar(30) NULL
	,@ClaimId int output
)
AS
BEGIN TRAN
BEGIN TRY 
	INSERT INTO [dbo].[MediclaimCashless]
			   ([NameOfHospital]
			   ,[HospitalPhoneNumber]
			   ,[HospitalAddress]
			   ,[HospitalId]
			   ,[EmailId]
			   ,[NameOfPatient]
			   ,[Gender]
			   ,[PatientPhoneNumber]
			   ,[PatientAddress]
			   ,[DateOfBirth]
			   ,[Age]
			   ,[MedicalSectionPageNumber]
			   ,[NameOfCardHolder]
			   ,[MedicalCardNumber]
			   ,[AdmissionNumber]
			   ,[CardCategory]
			   ,[CaseType]
			   ,[TypeOfTreatment]
			   ,[Amount]
			   ,[DateOfAdmission]
			   ,[DateOfDischargeOrDeath]
			   ,[MedicalCard]
			   ,[DoctorPrescription]
			   ,[OriginalBill]
			   ,[BillCashMemo]
			   ,[CreatedBy])
		 VALUES
			   (@NameOfHospital
				,@HospitalPhoneNumber 
				,@HospitalAddress
				,@HospitalId
				,@EmailId
				,@NameOfPatient
				,@Gender
				,@PatientPhoneNumber
				,@PatientAddress
				,@DateOfBirth
				,@Age
				,@MedicalSectionPageNumber
				,@NameOfCardHolder
				,@MedicalCardNumber
				,@AdmissionNumber
				,@CardCategory
				,@CaseType
				,@TypeOfTreatment
				,@Amount
				,@DateOfAdmission
				,@DateOfDischargeOrDeath
				,@MedicalCard
				,@DoctorPrescription
				,@OriginalBill
				,@BillCashMemo
				,@CreatedBy)
			COMMIT TRAN
			
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
/****** Object:  StoredProcedure [dbo].[SaveMediclaimNonCashless]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  StoredProcedure [dbo].[SaveMediclaimVoucher]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveMediclaimVoucher]
(
	@PayTo [NVARCHAR](30),
	@ApprovedAmount [MONEY],
	@AmountInWords [NVARCHAR](100),
	@EntryNo [INT],
	@PageNo [INT],
	@BankBranch [NVARCHAR](100),
	@AccountNumber[NVARCHAR](100),
	@BICCode [NVARCHAR](15),
	@IFSCCode [NVARCHAR](10),
	@CreatedBy [NVARCHAR](30),
	@VoucherId [INT] OUTPUT
)
AS
BEGIN TRAN
BEGIN TRY
	INSERT INTO dbo.MediclaimVoucher
	(PayTo, ApprovedAmount, AmountInWords, EntryNo, PageNo, BankBranch, AccountNumber, BICCode, IFSCCode, CreatedBy)
	 VALUES(@PayTo, @ApprovedAmount, @AmountInWords, @EntryNo, @PageNo,  @BankBranch,  @AccountNumber, @BICCode, @IFSCCode, @CreatedBy)
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
/****** Object:  StoredProcedure [dbo].[SaveOPDCND]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveOPDCND]
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
/****** Object:  StoredProcedure [dbo].[UpdateCashlessPhysicalSubmit]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCashlessPhysicalSubmit]
(
		@ClaimId int,
		@PhysicalSubmit bit,	
		@ModifiedBy nvarchar(30) NULL
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[MediclaimCashless] 
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
/****** Object:  StoredProcedure [dbo].[UpdateNonCashlessPhysicalSubmit]    Script Date: 09-12-2021 09:02:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
