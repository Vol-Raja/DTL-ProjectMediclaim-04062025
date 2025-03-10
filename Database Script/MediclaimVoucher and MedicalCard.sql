CREATE TABLE dbo.MediclaimVoucher
(
	VoucherId [INT] IDENTITY(1,1),
	PayTo [NVARCHAR](30) NOT NULL,
	ApprovedAmount [MONEY] NOT NULL,
	AmountInWords [NVARCHAR](100),
	EntryNo [INT] NOT NULL,
	PageNo [INT] NOT NULL,
	BankBranch [NVARCHAR](100),
	AccountNumber[NVARCHAR](100),
	BICCode [NVARCHAR](15),
	IFSCCode [NVARCHAR](10),
	CreatedBy [NVARCHAR](30) NOT NULL,
	CreatedDate [DATE] NOT NULL DEFAULT GETDATE(),
	ModifiedBy [NVARCHAR](30) NULL,
	ModifiedDate [DATE] NULL,
	CONSTRAINT PK_VoucherId PRIMARY KEY(VoucherId)
)



CREATE PROCEDURE dbo.SaveMediclaimVoucher
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




CREATE PROCEDURE dbo.GetMediclaimVoucherByParam
(
	@VoucherId [INT] = NULL,
	@PayTo [NVARCHAR](30) = NULL,
	@EntryNo [INT] = NULL,
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




CREATE TABLE dbo.MedicalCard
(
	MedicalCardId [INT] IDENTITY(1,1),
	CardNo [NVARCHAR](30) NOT NULL,
	EmployeeNo [NVARCHAR](30) NOT NULL,
	PPONo [NVARCHAR](30) NOT NULL,
	NameOfCardHolder [NVARCHAR](30) NOT NULL,
	Employer [NVARCHAR](30) NOT NULL,
	DateOfBirth [DATE] NOT NULL,
	Age [INT] NOT NULL,
	Gender [NVARCHAR](6) NOT NULL,
	MedicalSectionPageNo [INT] NOT NULL,
	CardCategory [NVARCHAR](13) NOT NULL,
	MobileNumber [NVARCHAR](10) NOT NULL,
	Address [NVARCHAR](150) NOT NULL,
	BankName [NVARCHAR](50) NOT NULL,
	BICCode [NVARCHAR](15) NOT NULL,
	IFSCCode [NVARCHAR](10) NOT NULL,
	AccountNumber[NVARCHAR](100) NOT NULL,
	CreatedBy [NVARCHAR](30) NOT NULL,
	CreatedDate [DATE] NOT NULL DEFAULT GETDATE(),
	ModifiedBy [NVARCHAR](30) NULL,
	ModifiedDate [DATE] NULL,
	CONSTRAINT PK_MedicalCardId PRIMARY KEY(MedicalCardId)
)


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
	@IFSCCode [NVARCHAR](10),
	@AccountNumber[NVARCHAR](100),
	@CreatedBy [NVARCHAR](30),
	@ModifiedBy [NVARCHAR](30),
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
		CreatedBy,
		ModifiedBy)
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
		@CreatedBy,
		@ModifiedBy)
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



CREATE PROCEDURE dbo.GetMedicalCardByParam
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