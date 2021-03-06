/****** Object:  StoredProcedure [dbo].[SP_UpdateCustomerBankAccount]    Script Date: 06/24/2009 12:06:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCustomerBankAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateCustomerBankAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustomerBankAccount]    Script Date: 06/24/2009 12:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCustomerBankAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_UpdateCustomerBankAccount]  
@CB_CustBankAccId int,  
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
@CB_IFSC varchar(11)  
as  
update CustomerBank set   
                               CB_BankName=@CB_BankName,  
                               XBAT_BankAccountTypeCode=@XBAT_BankAccountTypeCode,  
                               CB_AccountNum=@CB_AccountNum,  
                               XMOH_ModeOfHoldingCode=@XMOH_ModeOfHoldingCode,  
                               CB_BranchName=@CB_BranchName,  
                               CB_BranchAdrLine1=@CB_BranchAdrLine1,  
                               CB_BranchAdrLine2=@CB_BranchAdrLine2,  
                               CB_BranchAdrLine3=@CB_BranchAdrLine3,  
                               CB_BranchAdrPinCode=@CB_BranchAdrPinCode,  
                               CB_BranchAdrCity=@CB_BranchAdrCity,  
                               CB_BranchAdrState=@CB_BranchAdrState,  
                               CB_BranchAdrCountry=@CB_BranchAdrCountry,  
                               CB_Balance=@CB_Balance,  
                               CB_MICR=@CB_MICR,  
                               CB_IFSC=@CB_IFSC  
                                 
                               where C_CustomerId=@C_CustomerId and CB_CustBankAccId=@CB_CustBankAccId' 
END
GO
