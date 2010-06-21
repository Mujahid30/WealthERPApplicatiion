--SET QUOTED_IDENTIFIER ON
--GO

ALTER PROCEDURE [dbo].[SP_EquityMappingToScripMapping]
(
@Mode varchar(10),
@WERPCODE int,
@ScripName varchar(255),
@Ticker varchar(100),
@IncorporationDate datetime,
@PublicIssueDate datetime,
@MarketLot int,
@FaceValue numeric(18,4),
@BookClosure datetime,
@InstrumentCategory varchar(4),
@SubCategory varchar(6),
--@Sector int ,
--@MarketCap int,
@BSE varchar(25),
@NSE varchar(25),
@CERC varchar(25)
)
AS
BEGIN
	--SET NOCOUNT ON;
	Declare @ScripCode int ;
	Declare @BSECODE varchar(25);
	Declare @NSECODE varchar(25);
	Declare @CERCCODE varchar(25);
	Declare @bTran AS INT;   
	Declare	@lErrCode AS INT
SET NOCOUNT ON  ;
		-- Begin Tran
	If (@@Trancount = 0)  
	 Begin  
	  Set @bTran = 1  
	  Begin Transaction  
	 End   
	
	
IF (@Mode='New')
  
BEGIN
    
	Insert INTO ProductEquityMaster
	(
	 PEM_CompanyName,
	-- PSC_SectorId,
	-- PMCC_MarketCapClassificationCode,
	 PEM_MarketLot,
	 PEM_FaceValue,
	 PEM_BookClosure,
	 PEM_Incorporation,
	 PEM_PublicIssueDate,
	 PEM_Ticker,
	 PAISC_AssetInstrumentSubCategoryCode,
	 PAIC_AssetInstrumentCategoryCode,
	 PAG_AssetGroupCode
	)
	Values
	(
	@ScripName,@MarketLot,
	@FaceValue,@BookClosure,@IncorporationDate,
	@PublicIssueDate,@Ticker,
	@SubCategory,@InstrumentCategory,'DE'
	 )
	 
	 Select @ScripCode = Scope_Identity()
	 
	 
	 
if(@BSE is not null)
BEGIN
	 Insert into ProductEquityScripMapping
	 (
	 PEM_ScripCode,
	 PESM_IdentifierType,
	 PESM_IdentifierName,
	 PESM_Identifier
	 )
	 Values
	 (
	 @ScripCode,'Exchange','BSE',@BSE
	 )
END
	 
if(@NSE is not null)
BEGIN
	 Insert into ProductEquityScripMapping
	 (
	 PEM_ScripCode,
	 PESM_IdentifierType,
	 PESM_IdentifierName,
	 PESM_Identifier
	 )
	 Values
	 (
	 @ScripCode,'Exchange','NSE',@NSE
	 )
END
	 
if(@CERC is not null)
BEGIN
	 Insert into ProductEquityScripMapping
	 (
	 PEM_ScripCode,
	 PESM_IdentifierType,
	 PESM_IdentifierName,
	 PESM_Identifier
	 )
	 Values
	 (
	 @ScripCode,'ASIAN_CERC','CERC',@CERC
	 )
END

Select  @ScripCode 

END

--Update if WERPCODE IS NOT NULL

ELSE IF (@Mode ='Edit')
  
BEGIN
  
					Update ProductEquityMaster
					SET
						 PEM_CompanyName =@ScripName,
						-- PSC_SectorId=@Sector,
						-- PMCC_MarketCapClassificationCode=@MarketCap,
						 PEM_MarketLot=@MarketLot,
						 PEM_FaceValue=@FaceValue,
						 PEM_BookClosure=@BookClosure,
						 PEM_Incorporation=@IncorporationDate,
						 PEM_PublicIssueDate=@PublicIssueDate,
						 PEM_Ticker=@Ticker,
						 PAISC_AssetInstrumentSubCategoryCode=@SubCategory,
						 PAIC_AssetInstrumentCategoryCode=@InstrumentCategory,
						 PAG_AssetGroupCode='DE'
					WHERE PEM_ScripCode = @WERPCODE	 


				Select @BSECODE = PESM_Identifier from ProductEquityScripMapping 
								  where PEM_ScripCode = @WERPCODE 
								  AND PESM_IdentifierName ='BSE'
				Select @NSECODE = PESM_Identifier from ProductEquityScripMapping 
								  where PEM_ScripCode = @WERPCODE 
								  AND PESM_IdentifierName ='NSE'
				Select @CERCCODE = PESM_Identifier from ProductEquityScripMapping 
								  where PEM_ScripCode = @WERPCODE 
								  AND PESM_IdentifierName ='CERC'     
                                                

		if(@BSECODE is not null and @BSE <> @BSECODE and @BSE is not null)
		 BEGIN
				  Update ProductEquityScripMapping
				  SET PESM_Identifier = @BSE 
				  where PEM_ScripCode = @WERPCODE 
				  AND PESM_IdentifierName='BSE'
		 END
		else if(@BSE is not null and @BSECODE is null)
      BEGIN
				 Insert into ProductEquityScripMapping
				 (
				 PEM_ScripCode,
				 PESM_IdentifierType,
				 PESM_IdentifierName,
				 PESM_Identifier
				 )
				 Values
				 (
				 @WERPCODE,'Exchange','BSE',@BSE
				 )
		END
		else if(@BSE is null and @BSECODE is not null)
	  BEGIN
				  DELETE from ProductEquityScripMapping
				  where PEM_ScripCode= @WERPCODE
				  AND PESM_Identifier = @BSECODE
				  AND PESM_IdentifierName='BSE'
				  print 'Sumit'
	  END 
      
     
	if(@NSECODE is not null and @NSE <> @NSECODE and @NSE is not null)
     BEGIN
			 Update ProductEquityScripMapping
			  SET PESM_Identifier = @NSE 
			  where PEM_ScripCode = @WERPCODE 
			  AND PESM_IdentifierName='NSE'
    END
   else if(@NSE is not null and @NSECODE is null)
      BEGIN
				 Insert into ProductEquityScripMapping
				 (
				 PEM_ScripCode,
				 PESM_IdentifierType,
				 PESM_IdentifierName,
				 PESM_Identifier
				 )
				 Values
				 (
				 @WERPCODE,'Exchange','NSE',@NSE
				 )
	 END
	else if(@NSE is null and @NSECODE<> null)
	  BEGIN
			  DELETE from ProductEquityScripMapping
			  where PEM_ScripCode= @WERPCODE
			  AND PESM_Identifier = @NSECODE
			  AND PESM_IdentifierName='NSE'
	  END 
      ------
      
       If(@CERCCODE is not null and @CERC <> @CERCCODE and @CERC is not null)
     BEGIN
			  Update ProductEquityScripMapping
			  SET PESM_Identifier = @CERC 
			  where PEM_ScripCode = @WERPCODE 
			  AND PESM_IdentifierName='CERC'
    END
   else if(@CERC is not null and @CERCCODE is null)
      BEGIN
			 Insert into ProductEquityScripMapping
			 (
			 PEM_ScripCode,
			 PESM_IdentifierType,
			 PESM_IdentifierName,
			 PESM_Identifier
			 )
			 Values
			 (
			 @WERPCODE,'ASIAN_CERC','CERC',@CERC
			 )
	  END
	else if(@CERC is null and @CERCCODE is not null)
			  BEGIN
			  DELETE from ProductEquityScripMapping
			  where PEM_ScripCode= @WERPCODE
			  AND PESM_Identifier = @CERCCODE
			  AND PESM_IdentifierName='CERC'
			  END 
      
 END 
 Select @WERPCODE
 
 If (@@Error <> 0)                    
	Begin                    
	  Set @lErrCode = 1001 -- This is an error code set by the application     
	  Goto Error                    
	End  

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
SET NOCOUNT OFF ;   
END

 