-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateCompleteCustomer]
@ADUL_ProcessId	int,
@C_CustCode	varchar(10),
@AR_RMId int,
@C_ProfilingDate	datetime,
@C_FirstName	varchar(25),
@C_MiddleName	varchar(25),
@C_LastName	varchar(75),
@C_Gender	varchar(10),
@C_DOB	datetime,
@XCT_CustomerTypeCode	varchar(5),
@XCST_CustomerSubTypeCode	varchar(5),
@C_Salutation	varchar(5),
@C_PANNum	varchar(10),
@C_Adr1Line1	varchar(75),
@C_Adr1Line2	varchar(75),
@C_Adr1Line3	varchar(75),
@C_Adr1PinCode	numeric(10, 0),
@C_Adr1City	varchar(25),
@C_Adr1State	varchar(25),
@C_Adr1Country	varchar(25),
@C_Adr2Line1	varchar(75),
@C_Adr2Line2	varchar(75),
@C_Adr2Line3	varchar(75),
@C_Adr2PinCode	numeric(10, 0),
@C_Adr2City	varchar(20),
@C_Adr2State	varchar(20),
@C_Adr2Country	varchar(20),
@C_ResISDCode	numeric(5, 0),
@C_ResSTDCode	numeric(5, 0),
@C_ResPhoneNum	numeric(25, 0),
@C_OfcISDCode	numeric(5, 0),
@C_OfcSTDCode	numeric(5, 0),
@C_OfcPhoneNum	numeric(25, 0),
@C_Email	varchar(75),
@C_AltEmail	varchar(75),
@C_Mobile1	numeric(14, 0),
@C_Mobile2	numeric(14, 0),
@C_ISDFax	numeric(5, 0),
@C_STDFax	numeric(5, 0),
@C_Fax	numeric(25, 0),
@C_OfcFax	numeric(25, 0),
@C_OfcFaxISD	numeric(5, 0),
@C_OfcFaxSTD	numeric(5, 0),
@XO_OccupationCode	varchar(5),
@XQ_QualificationCode	varchar(5),
@C_MarriageDate	datetime,
@XMS_MaritalStatusCode	varchar(5),
@XN_NationalityCode	varchar(5),
@C_RBIRefNum	varchar(25),
@C_RBIApprovalDate	datetime,
@C_CompanyName	varchar(50),
@C_OfcAdrLine1	varchar(75),
@C_OfcAdrLine2	varchar(75),
@C_OfcAdrLine3	varchar(75),
@C_OfcAdrPinCode	numeric(10, 0),
@C_OfcAdrCity	varchar(25),
@C_OfcAdrState	varchar(25),
@C_OfcAdrCountry	varchar(25),
@C_RegistrationDate	datetime,
@C_CommencementDate	datetime,
@C_RegistrationPlace	varchar(20),
@C_RegistrationNum	varchar(25),
@C_CompanyWebsite	varchar(25),
@C_CreatedBy	int,

@C_ModifiedBy	int,
@U_Password	varchar(50),
@U_FirstName	varchar(50),
@U_MiddleName	varchar(50),
@U_LastName	varchar(50),
@U_Email	varchar(MAX),
@U_UserType	varchar(10),
@U_LoginId	varchar(MAX),
@CP_PortfolioName	varchar(50),
@CP_IsMainPortfolio	tinyint,
@CP_IsPMS	tinyint,
@CP_PMSIdentifier	varchar(20),
@U_UserId BIGINT OUTPUT,
@CP_PortfolioId BIGINT OUTPUT,
@C_CustomerId BIGINT OUTPUT

AS



SET NOCOUNT ON   

Declare  
  @bTran AS INT,      
  @lErrCode AS INT

-- Begin Tran
If (@@Trancount = 0)  
 Begin  
  Set @bTran = 1  
  Begin Transaction  
 End   
--DECLARE 
--		@u_UserId AS INT,
--		@c_CustomerId AS INT,
--		@cp_PortfolioId AS INT


exec SP_CreateUser 
					@U_Password,
					@U_FirstName,
					@U_MiddleName,
					@U_LastName,
					@U_Email,
					@U_UserType,
					@U_LoginId,
					
					@C_CreatedBy,
					
					@C_ModifiedBy,
					@U_UserId OUT
					


exec SP_CreateCustomer
					@C_CustCode,
					@AR_RMId,
					@U_UserId,
					@C_ProfilingDate,
					@C_FirstName,
					@C_MiddleName,
					@C_LastName,
					@C_Gender,
					@C_DOB,
					@XCT_CustomerTypeCode,
					@XCST_CustomerSubTypeCode,
					@C_Salutation,
					@C_PANNum,
					@C_Adr1Line1,
					@C_Adr1Line2,
					@C_Adr1Line3,
					@C_Adr1PinCode,
					@C_Adr1City,
					@C_Adr1State,
					@C_Adr1Country,
					@C_Adr2Line1,
					@C_Adr2Line2,
					@C_Adr2Line3,
					@C_Adr2PinCode,
					@C_Adr2City,
					@C_Adr2State,
					@C_Adr2Country,
					@C_ResISDCode,
					@C_ResSTDCode,
					@C_ResPhoneNum,
					@C_OfcISDCode,
					@C_OfcSTDCode,
					@C_OfcPhoneNum,
					@C_Email,
					@C_AltEmail,
					@C_Mobile1,
					@C_Mobile2,
					@C_ISDFax,
					@C_STDFax,
					@C_Fax,
					@C_OfcFaxISD,
					@C_OfcFaxSTD,
					@C_OfcFax,
					@XO_OccupationCode,
					@XQ_QualificationCode,
					--@C_MarriageDate,
					@XMS_MaritalStatusCode,	
					@XN_NationalityCode,
					@C_RBIRefNum,
					@C_RBIApprovalDate,
					@C_CompanyName,
					@C_OfcAdrLine1,
					@C_OfcAdrLine2,
					@C_OfcAdrLine3,
					@C_OfcAdrPinCode,
					@C_OfcAdrCity,
					@C_OfcAdrState,
					@C_OfcAdrCountry,
					@C_RegistrationDate,
					@C_CommencementDate,
					@C_RegistrationPlace,
					@C_RegistrationNum,
					@C_CompanyWebsite,

					@C_CreatedBy,
					@C_ModifiedBy,
					@C_CustomerId OUT

exec SP_CreateCustomerPortfolio
					
					@C_CustomerId,
					@CP_IsMainPortfolio,
					@CP_IsPMS ,
					@CP_PMSIdentifier ,
					@C_CreatedBy,
					@C_ModifiedBy ,
					@CP_PortfolioId OUT

Success:      
 If (@bTran = 1 And @@Trancount > 0)      
 Begin                                    
  Commit Tran      
 End      
 Return 0      
      
 Goto Done      
      
Error:      
 If (@bTran = 1 And @@Trancount > 0)      
 Begin      
  Rollback Transaction      
 End      
 Return @lErrCode      

	       
Done:            
SET NOCOUNT OFF       


 