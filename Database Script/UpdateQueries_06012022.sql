USE [DTLPension]
GO

/****** Object:  Table [dbo].[MediclaimVoucher]    Script Date: 06-01-2022 20:47:59 ******/
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
	[ClaimId] [int] NOT NULL,
	[ClaimType] [nvarchar](12) NULL,
	[IsDelete] [bit] NOT NULL,
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

ALTER TABLE [dbo].[MediclaimVoucher] ADD  CONSTRAINT [DF_MediclaimVoucher_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[MediclaimVoucher] ADD  CONSTRAINT [DF_MediclaimVoucher_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO

GO
/****** Object:  StoredProcedure [dbo].[GetMediclaimVoucherByParam]    Script Date: 06-01-2022 20:52:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetMediclaimVoucherByParam]
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
		IFSCCode,
		ClaimId,
		ClaimType
	
	FROM dbo.MediclaimVoucher
	WHERE
		((VoucherId = @VoucherId) OR @VoucherId IS NULL)
		AND ((PayTo LIKE '%'+@PayTo+'%') OR @PayTo IS NULL)
		AND ((EntryNo = @EntryNo) OR @EntryNo IS NULL)
		AND ((PageNo = @PageNo) OR @PageNo IS NULL)
		AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)
	
END

GO
/****** Object:  StoredProcedure [dbo].[SaveMediclaimVoucher]    Script Date: 06-01-2022 20:54:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SaveMediclaimVoucher]
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
	@ClaimId [INT],
	@ClaimType [NVARCHAR](8) = NULL,
	@CreatedBy [NVARCHAR](30),
	@VoucherId [INT] OUTPUT
)
AS
BEGIN TRAN
BEGIN TRY
	INSERT INTO dbo.MediclaimVoucher
	(PayTo, ApprovedAmount, AmountInWords, EntryNo, PageNo, BankBranch, AccountNumber, BICCode, IFSCCode, ClaimId, ClaimType, CreatedBy)
	 VALUES(@PayTo, @ApprovedAmount, @AmountInWords, @EntryNo, @PageNo,  @BankBranch,  @AccountNumber, @BICCode, @IFSCCode, @ClaimId, @ClaimType, @CreatedBy)
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


