
alter table dbo.gpfwithdraw alter column Employer nvarchar(100)

USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[UpdateGPFWithdrawal]    Script Date: 04-01-2022 20:18:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateGPFWithdrawal]
(
	@WithdrawId  INT,
	@WithdrawType NVARCHAR(13),
	@AccountHolderName NVARCHAR(50),
	@EmployeId NVARCHAR(15),
	@Designation NVARCHAR(20),
	@Employer NVARCHAR(100),
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
