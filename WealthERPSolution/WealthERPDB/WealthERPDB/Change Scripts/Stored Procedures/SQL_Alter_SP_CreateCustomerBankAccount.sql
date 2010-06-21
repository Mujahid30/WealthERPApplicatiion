-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER PROCEDURE [dbo].[SP_CreateCustomerBankAccount]  
@CB_BankName varchar(25),  
@C_CustomerId int,  
@XBAT_BankAccountTypeCode varchar(25),  
@CB_AccountNum varchar(50),  
@XMOH_ModeOfHoldingCode varchar(25),  
@CB_BranchName varchar(50),  
@CB_BranchAdrLine1 varchar(25),  
@CB_BranchAdrLine2 varchar(25),  
@CB_BranchAdrLine3 varchar(25),  
@CB_BranchAdrPinCode numeric(6,0),  
@CB_BranchAdrCity varchar(25),  
@CB_BranchAdrState varchar(25),  
@CB_BranchAdrCountry varchar(25),  
@CB_Balance NUMERIC(18,3),  
@CB_MICR numeric(9,0),  
@CB_IFSC varchar(11),  
@CB_CreatedBy int ,  
@CB_ModifiedBy int  
as  
insert into dbo.CustomerBank(  
CB_BankName,  
C_CustomerId,  
XBAT_BankAccountTypeCode,  
CB_AccountNum,  
XMOH_ModeOfHoldingCode,  
CB_BranchName,  
CB_BranchAdrLine1,  
CB_BranchAdrLine2,  
CB_BranchAdrLine3,  
CB_BranchAdrPinCode,  
CB_BranchAdrCity,  
CB_BranchAdrState,  
CB_BranchAdrCountry,  
CB_Balance,  
CB_MICR,  
CB_IFSC,  
CB_CreatedBy,  
CB_CreatedOn,  
CB_ModifiedBy,  
CB_ModifiedOn  
)  
 values  
(  
@CB_BankName,  
@C_CustomerId,  
@XBAT_BankAccountTypeCode,  
@CB_AccountNum,  
@XMOH_ModeOfHoldingCode,  
@CB_BranchName,  
@CB_BranchAdrLine1,  
@CB_BranchAdrLine2,  
@CB_BranchAdrLine3,  
@CB_BranchAdrPinCode,  
@CB_BranchAdrCity,  
@CB_BranchAdrState,  
@CB_BranchAdrCountry,  
@CB_Balance,  
@CB_MICR,  
@CB_IFSC,  
@CB_CreatedBy,  
current_timestamp,  
@CB_ModifiedBy,  
current_timestamp)   