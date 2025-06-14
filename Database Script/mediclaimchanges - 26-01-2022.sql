USE [DTLPension1]
GO
/****** Object:  StoredProcedure [dbo].[UpdateCashless]    Script Date: 27-01-2022 02:14:53 PM ******/
DROP PROCEDURE [dbo].[UpdateCashless]
GO
/****** Object:  StoredProcedure [dbo].[SaveMediclaimCashless]    Script Date: 27-01-2022 02:14:53 PM ******/
DROP PROCEDURE [dbo].[SaveMediclaimCashless]
GO
/****** Object:  StoredProcedure [dbo].[SaveMedicalPageDetail]    Script Date: 27-01-2022 02:14:53 PM ******/
DROP PROCEDURE [dbo].[SaveMedicalPageDetail]
GO
/****** Object:  StoredProcedure [dbo].[SaveMedicalCard]    Script Date: 27-01-2022 02:14:53 PM ******/
DROP PROCEDURE [dbo].[SaveMedicalCard]
GO
/****** Object:  StoredProcedure [dbo].[GetMedicalPageDetailByParam]    Script Date: 27-01-2022 02:14:53 PM ******/
DROP PROCEDURE [dbo].[GetMedicalPageDetailByParam]
GO
/****** Object:  StoredProcedure [dbo].[GetMedicalCardByParam]    Script Date: 27-01-2022 02:14:53 PM ******/
DROP PROCEDURE [dbo].[GetMedicalCardByParam]
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessListByCreatedBy]    Script Date: 27-01-2022 02:14:53 PM ******/
DROP PROCEDURE [dbo].[GetCashlessListByCreatedBy]
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessListByClaimId]    Script Date: 27-01-2022 02:14:53 PM ******/
DROP PROCEDURE [dbo].[GetCashlessListByClaimId]
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessClaimByParam]    Script Date: 27-01-2022 02:14:53 PM ******/
DROP PROCEDURE [dbo].[GetCashlessClaimByParam]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_IsActive]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_CreatedDate]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_PhysicalSubmit]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_ClaimStatusId]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_ImplantBill]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_StentBill]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_Investigations]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_FormB]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_ReferralLetter]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_MPC]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_DoctorPrescription]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_Bills]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_PhotoId]
GO
ALTER TABLE [dbo].[MediclaimCashless] DROP CONSTRAINT [DF_MediclaimCashless_MedicalCard]
GO
ALTER TABLE [dbo].[MedicalPageDetail] DROP CONSTRAINT [DF_MedicalPageDetail_IsDelete]
GO
ALTER TABLE [dbo].[MedicalPageDetail] DROP CONSTRAINT [DF__MedicalPa__Creat__7E37BEF6]
GO
ALTER TABLE [dbo].[MedicalCard] DROP CONSTRAINT [DF_MedicalCard_IsDelete]
GO
ALTER TABLE [dbo].[MedicalCard] DROP CONSTRAINT [DF__MedicalCa__Creat__6CA31EA0]
GO
/****** Object:  Table [dbo].[MediclaimCashless]    Script Date: 27-01-2022 02:14:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MediclaimCashless]') AND type in (N'U'))
DROP TABLE [dbo].[MediclaimCashless]
GO
/****** Object:  Table [dbo].[MedicalPageDetail]    Script Date: 27-01-2022 02:14:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MedicalPageDetail]') AND type in (N'U'))
DROP TABLE [dbo].[MedicalPageDetail]
GO
/****** Object:  Table [dbo].[MedicalCard]    Script Date: 27-01-2022 02:14:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MedicalCard]') AND type in (N'U'))
DROP TABLE [dbo].[MedicalCard]
GO
/****** Object:  Table [dbo].[MedicalCard]    Script Date: 27-01-2022 02:14:54 PM ******/
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
	[NameOfDependent] [nvarchar](50) NULL,
	[RelationWithRetiree] [nvarchar](50) NULL,
	[DependentDob] [date] NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_MedicalCardId] PRIMARY KEY CLUSTERED 
(
	[MedicalCardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalPageDetail]    Script Date: 27-01-2022 02:14:54 PM ******/
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
	[NameOfDependent] [nvarchar](50) NULL,
	[RelationWithRetiree] [nvarchar](50) NULL,
	[DependentDob] [date] NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [nvarchar](50) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_PageDetailId] PRIMARY KEY CLUSTERED 
(
	[PageDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediclaimCashless]    Script Date: 27-01-2022 02:14:54 PM ******/
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
	[NameOfPatient] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](6) NOT NULL,
	[PatientPhoneNumber] [nvarchar](16) NOT NULL,
	[PatientAddress] [nvarchar](250) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Age] [nvarchar](50) NOT NULL,
	[MedicalSectionPageNumber] [nvarchar](50) NOT NULL,
	[NameOfCardHolder] [nvarchar](80) NOT NULL,
	[MedicalCardNumber] [nvarchar](16) NOT NULL,
	[AdmissionNumber] [nvarchar](50) NOT NULL,
	[CardCategory] [nvarchar](20) NOT NULL,
	[CaseType] [nvarchar](50) NOT NULL,
	[TypeOfTreatment] [nvarchar](50) NOT NULL,
	[Amount] [money] NOT NULL,
	[DateOfAdmission] [date] NOT NULL,
	[DateOfDischargeOrDeath] [date] NOT NULL,
	[MedicalCard] [bit] NULL,
	[BillCashMemo] [bit] NULL,
	[OriginalBill] [bit] NULL,
	[CoveringLetter] [bit] NULL,
	[MPC] [bit] NULL,
	[ReferralLetter] [bit] NULL,
	[FormB] [bit] NULL,
	[Investigations] [bit] NULL,
	[StentBill] [bit] NULL,
	[ImplantBill] [bit] NULL,
	[ClaimStatusId] [int] NOT NULL,
	[RejectReason] [nvarchar](200) NULL,
	[PhysicalSubmit] [bit] NOT NULL,
	[ApprovedAmount] [money] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_MediclaimCashless] PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MedicalCard] ADD  CONSTRAINT [DF__MedicalCa__Creat__6CA31EA0]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MedicalCard] ADD  CONSTRAINT [DF_MedicalCard_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[MedicalPageDetail] ADD  CONSTRAINT [DF__MedicalPa__Creat__7E37BEF6]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MedicalPageDetail] ADD  CONSTRAINT [DF_MedicalPageDetail_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_MedicalCard]  DEFAULT ((0)) FOR [MedicalCard]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_PhotoId]  DEFAULT ((0)) FOR [BillCashMemo]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_Bills]  DEFAULT ((0)) FOR [OriginalBill]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_DoctorPrescription]  DEFAULT ((0)) FOR [CoveringLetter]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_MPC]  DEFAULT ((0)) FOR [MPC]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_ReferralLetter]  DEFAULT ((0)) FOR [ReferralLetter]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_FormB]  DEFAULT ((0)) FOR [FormB]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_Investigations]  DEFAULT ((0)) FOR [Investigations]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_StentBill]  DEFAULT ((0)) FOR [StentBill]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_ImplantBill]  DEFAULT ((0)) FOR [ImplantBill]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_ClaimStatusId]  DEFAULT ((1)) FOR [ClaimStatusId]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_PhysicalSubmit]  DEFAULT ((0)) FOR [PhysicalSubmit]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_IsActive]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessClaimByParam]    Script Date: 27-01-2022 02:14:54 PM ******/
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
	[BillCashMemo],
	[OriginalBill],
	[CoveringLetter],
	[MPC],
	[ReferralLetter],
	[FormB],
	[Investigations],
	[StentBill],
	[ImplantBill],
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
	AND (IsDelete = 0)
END
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessListByClaimId]    Script Date: 27-01-2022 02:14:54 PM ******/
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
	[BillCashMemo],
	[OriginalBill],
	[CoveringLetter],
	[MPC],
	[ReferralLetter],
	[FormB],
	[Investigations],
	[StentBill],
	[ImplantBill],
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
	[CLaimId] = @ClaimId AND
	[IsDelete] = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessListByCreatedBy]    Script Date: 27-01-2022 02:14:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCashlessListByCreatedBy]
(
	@CreatedBy nvarchar(50)
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
	  ,[BillCashMemo]
	  ,[OriginalBill]
	  ,[CoveringLetter]
	  ,[MPC]
	  ,[ReferralLetter]
	  ,[FormB]
	  ,[Investigations]
	  ,[StentBill]
	  ,[ImplantBill]
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
	[CreatedBy] = @CreatedBy AND
	[IsDelete] = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetMedicalCardByParam]    Script Date: 27-01-2022 02:14:54 PM ******/
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
		NameOfDependent,
		RelationWithRetiree,
		DependentDob,
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
		AND (IsDelete = 0)
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetMedicalPageDetailByParam]    Script Date: 27-01-2022 02:14:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMedicalPageDetailByParam]
	(
		@PageDetailId INT = NULL,
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
			 [Name],
			 PPONumber,
			 Designation,
			 Department,
			 CardCategory,
			 MobileNumber,
			 DateOfRetirement,
			 SpouseName,
			 LBP,
			 Dispensary,			 
			 NameOfDependent,
			 RelationWithRetiree,
			 DependentDob,
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
			AND ((PageDetailId = @PageDetailId) OR @PageDetailId IS NULL)
			AND (IsDelete = 0)
	
	END
GO
/****** Object:  StoredProcedure [dbo].[SaveMedicalCard]    Script Date: 27-01-2022 02:14:54 PM ******/
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
	@AccountNumber [NVARCHAR](100),
	@NameOfDependent [NVARCHAR](50),
	@RelationWithRetiree [NVARCHAR](50),
	@DependentDob [DATE],
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
		NameOfDependent,
		RelationWithRetiree,
		DependentDob,
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
		@NameOfDependent,
		@RelationWithRetiree,
		@DependentDob,
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
/****** Object:  StoredProcedure [dbo].[SaveMedicalPageDetail]    Script Date: 27-01-2022 02:14:54 PM ******/
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
	@NameOfDependent NVARCHAR(50),
	@RelationWithRetiree NVARCHAR(50),
	@DependentDob DATE,
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
		 [Name],
		 PPONumber,
		 Designation,
		 Department,
		 CardCategory,
		 MobileNumber,
		 DateOfRetirement,
		 SpouseName,
		 LBP,
		 Dispensary,
		 NameOfDependent,
		 RelationWithRetiree,
		 DependentDob,
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
			@NameOfDependent,
			@RelationWithRetiree,
			@DependentDob,
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
/****** Object:  StoredProcedure [dbo].[SaveMediclaimCashless]    Script Date: 27-01-2022 02:14:54 PM ******/
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
	,@NameOfPatient nvarchar(50)
	,@Gender nvarchar(6)
	,@PatientPhoneNumber nvarchar(16)
	,@PatientAddress nvarchar(250)
	,@DateOfBirth date
	,@Age nvarchar(5)
	,@MedicalSectionPageNumber nvarchar(50)
	,@NameOfCardHolder nvarchar(80)
	,@MedicalCardNumber nvarchar(16)
	,@AdmissionNumber nvarchar(50)
	,@CardCategory nvarchar(20)
	,@CaseType nvarchar(50)
	,@TypeOfTreatment nvarchar(50)
	,@Amount money
	,@DateOfAdmission date
	,@DateOfDischargeOrDeath date
	,@MedicalCard bit
	,@BillCashMemo bit
	,@OriginalBill bit
	,@CoveringLetter bit
	,@MPC bit
	,@ReferralLetter bit
	,@FormB bit
	,@Investigations bit
	,@StentBill bit
	,@ImplantBill bit
	,@ClaimStatusId int
	,@CreatedBy nvarchar(50) NULL
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
			   ,[BillCashMemo]
			   ,[OriginalBill]
			   ,[CoveringLetter]
			   ,[MPC]
			   ,[ReferralLetter]
			   ,[FormB]
			   ,[Investigations]
			   ,[StentBill]
			   ,[ImplantBill]
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
				,@BillCashMemo
				,@OriginalBill
				,@CoveringLetter
				,@MPC
				,@ReferralLetter
				,@FormB
				,@Investigations
				,@StentBill
				,@ImplantBill
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
/****** Object:  StoredProcedure [dbo].[UpdateCashless]    Script Date: 27-01-2022 02:14:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCashless]
(
	@ClaimId int
	,@NameOfHospital nvarchar(100)
	,@HospitalPhoneNumber nvarchar(16) 
	,@HospitalAddress nvarchar(250)
	,@HospitalId nvarchar(15)
	,@EmailId nvarchar(50)
	,@NameOfPatient nvarchar(50)
	,@Gender nvarchar(6)
	,@PatientPhoneNumber nvarchar(16)
	,@PatientAddress nvarchar(250)
	,@DateOfBirth date
	,@Age nvarchar(5)
	,@MedicalSectionPageNumber nvarchar(50)
	,@NameOfCardHolder nvarchar(80)
	,@MedicalCardNumber nvarchar(16)
	,@AdmissionNumber nvarchar(50)
	,@CardCategory nvarchar(20)
	,@CaseType nvarchar(50)
	,@TypeOfTreatment nvarchar(50)
	,@Amount money
	,@DateOfAdmission date
	,@DateOfDischargeOrDeath date
	,@MedicalCard bit
	,@BillCashMemo bit
	,@OriginalBill bit
	,@CoveringLetter bit
	,@MPC bit
	,@ReferralLetter bit
	,@FormB bit
	,@Investigations bit
	,@StentBill bit
	,@ImplantBill bit
	,@ClaimStatusId int
	,@RejectReason nvarchar(200)
	,@ModifiedBy nvarchar(30)
)
As
BEGIN TRAN
BEGIN TRY 
	UPDATE [dbo].[MediclaimCashless]
	   SET [NameOfHospital] = @NameOfHospital
		  ,[HospitalPhoneNumber] = @HospitalPhoneNumber
		  ,[HospitalAddress] = @HospitalAddress
		  ,[HospitalId] = @HospitalId
		  ,[EmailId] = @EmailId
		  ,[NameOfPatient] = @NameOfPatient
		  ,[Gender] = @Gender
		  ,[PatientPhoneNumber] = @PatientPhoneNumber
		  ,[PatientAddress] = @PatientAddress
		  ,[DateOfBirth] = @DateOfBirth
		  ,[Age] = @Age
		  ,[MedicalSectionPageNumber] = @MedicalSectionPageNumber
		  ,[NameOfCardHolder] = @NameOfCardHolder
		  ,[MedicalCardNumber] = @MedicalCardNumber
		  ,[AdmissionNumber] = @AdmissionNumber
		  ,[CardCategory] = @CardCategory
		  ,[CaseType] = @CaseType
		  ,[TypeOfTreatment] = @TypeOfTreatment
		  ,[Amount] = @Amount
		  ,[DateOfAdmission] = @DateOfAdmission
		  ,[DateOfDischargeOrDeath] = @DateOfDischargeOrDeath
		  ,[MedicalCard] = @MedicalCard
		  ,[BillCashMemo] = @BillCashMemo
		  ,[OriginalBill] = @OriginalBill
		  ,[CoveringLetter]	= @CoveringLetter
		  ,[MPC] = @MPC
		  ,[ReferralLetter]	= @ReferralLetter
		  ,[FormB] = @FormB
		  ,[Investigations]	= @Investigations
		  ,[StentBill] = @StentBill
		  ,[ImplantBill] = @ImplantBill
		  ,[ClaimStatusId] = @ClaimStatusId
		  ,[ModifiedBy] = @ModifiedBy
		  ,[RejectReason] = NULL
		  ,[ModifiedDate] = GETDATE()
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
