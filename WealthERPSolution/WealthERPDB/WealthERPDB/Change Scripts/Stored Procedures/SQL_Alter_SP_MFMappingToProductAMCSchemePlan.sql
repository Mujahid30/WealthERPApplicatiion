ALTER PROCEDURE [dbo].[SP_MFMappingToProductAMCSchemePlan]
(
@Mode varchar(10),
@SchmPlanName varchar(max),
@InstrumentCategory varchar(8),
@SubCategory VARCHAR(6),
@SubSubCategory varchar(8),
--@Sector int,
--@MarketCap int,
@AMFI varchar(255) ,
@CAMS varchar(255) ,
@Karvy varchar(255),
@WERPCODE int
)
AS
 BEGIN
 SET NOCOUNT ON;
 Declare @SchemePlanCode int;
 DECLARE @AMFICODE varchar(255);
 DECLARE @KARVYCODE varchar(255);
 DECLARE @CAMSCODE  varchar(255);
 Declare @bTran AS INT;   
 Declare	@lErrCode AS INT
SET NOCOUNT ON  ;
		-- Begin Tran
	If (@@Trancount = 0)  
	 Begin  
	  Set @bTran = 1  
	  Begin Transaction  
	 End 
 
 IF(@Mode='New')
 
 BEGIN
 
 INSERT INTO ProductAMCSchemePlan
 (
 PASP_SchemePlanName,
 PAIC_AssetInstrumentCategoryCode,
 PAISC_AssetInstrumentSubCategoryCode,
 PAISSC_AssetInstrumentSubSubCategoryCode
 --PSC_SectorId,
 --PMCC_MarketCapClassificationCode
 )
 Values
 (
 @SchmPlanName,@InstrumentCategory,
 @SubCategory,@SubSubCategory
 )
 
 Select @SchemePlanCode = Scope_Identity()
 
 if(@AMFI is not null)
   BEGIN
   INSERT INTO ProductAMCSchemeMapping
    (
    PASP_SchemePlanCode,
    PASC_AMC_ExternalCode,
    PASC_AMC_ExternalType
    )
    Values
    (
     @WERPCode,@AMFI,'AMFI'
    )
   END
   
  if(@CAMS is not null)
   BEGIN
   INSERT INTO ProductAMCSchemeMapping
    (
    PASP_SchemePlanCode,
    PASC_AMC_ExternalCode,
    PASC_AMC_ExternalType
    )
    Values
    (
     @WERPCode,@CAMS,'CAMS'
    )
   END
   
  if(@Karvy is not null)
   BEGIN
   INSERT INTO ProductAMCSchemeMapping
    (
    PASP_SchemePlanCode,
    PASC_AMC_ExternalCode,
    PASC_AMC_ExternalType
    )
    Values
    (
     @WERPCode,@Karvy,'KARVY'
    )
   END
  Select @SchemePlanCode
END 
 
ELSE IF(@Mode='Edit')
  BEGIN
			   Update ProductAMCSchemePlan
				SET
				 PASP_SchemePlanName=@SchmPlanName,
				 PAIC_AssetInstrumentCategoryCode=@InstrumentCategory,
				 PAISC_AssetInstrumentSubCategoryCode=@SubCategory,
				 PAISSC_AssetInstrumentSubSubCategoryCode=@SubSubCategory
				 WHERE PASP_SchemePlanCode=@WERPCode
					 
				Select @AMFICODE = PASC_AMC_ExternalCode from ProductAMCSchemeMapping 
								  where PASP_SchemePlanCode = @WERPCODE 
								  AND PASC_AMC_ExternalType ='AMFI'
				Select @CAMSCODE = PASC_AMC_ExternalCode from ProductAMCSchemeMapping 
								  where PASP_SchemePlanCode = @WERPCODE 
								  AND PASC_AMC_ExternalType ='CAMS'
				Select @KARVYCODE = PASC_AMC_ExternalCode from ProductAMCSchemeMapping 
								  where PASP_SchemePlanCode = @WERPCODE 
								  AND PASC_AMC_ExternalType ='KARVY' 
								  
								  
                -----
         IF(@AMFICODE is not null and @AMFI <> @AMFICODE and @AMFI is not null)
		 BEGIN
				  Update ProductAMCSchemeMapping
				  SET PASC_AMC_ExternalCode = @AMFI 
				  where PASP_SchemePlanCode = @WERPCODE 
				  AND PASC_AMC_ExternalType='AMFI'
		 END
		ELSE IF(@AMFI is not null and @AMFICODE is null)
        BEGIN
				 Insert into ProductAMCSchemeMapping
				 (
				 PASP_SchemePlanCode,
				 PASC_AMC_ExternalCode,
				 PASC_AMC_ExternalType
				 )
				 Values
				 (
				 @WERPCode,@AMFI,'AMFI'
				 )
		END
		ELSE IF(@AMFI is null and @AMFICODE is not null)
	    BEGIN
				  DELETE from ProductAMCSchemeMapping
				  where PASP_SchemePlanCode= @WERPCODE
				  AND PASC_AMC_ExternalCode = @AMFICODE
				  AND PASC_AMC_ExternalType='AMFI'
	    END 
       
       ---CAMS
                
         IF(@CAMSCODE is not null and @CAMS <> @CAMSCODE and @CAMS is not null)
		 BEGIN
				  Update ProductAMCSchemeMapping
				  SET PASC_AMC_ExternalCode = @CAMS 
				  where PASP_SchemePlanCode = @WERPCODE 
				  AND PASC_AMC_ExternalType='CAMS'
		 END
		ELSE IF(@CAMS is not null and @CAMSCODE is null)
        BEGIN
				 Insert into ProductAMCSchemeMapping
				 (
				 PASP_SchemePlanCode,
				 PASC_AMC_ExternalCode,
				 PASC_AMC_ExternalType
				 )
				 Values
				 (
				 @WERPCODE,@CAMS,'CAMS'
				 )
		END
		ELSE IF(@CAMS is null and @CAMSCODE is not null)
	    BEGIN
				  DELETE from ProductAMCSchemeMapping
				  where PASP_SchemePlanCode= @WERPCODE
				  AND PASC_AMC_ExternalCode = @CAMSCODE
				  AND PASC_AMC_ExternalType='CAMS'
	    END 
              
        IF(@KARVYCODE is not null and @KARVY <> @KARVYCODE and @KARVY is not null)
		 BEGIN
				  Update ProductAMCSchemeMapping
				  SET PASC_AMC_ExternalCode = @KARVY 
				  where PASP_SchemePlanCode = @KARVYCODE 
				  AND PASC_AMC_ExternalType='KARVY'
		 END
		ELSE IF(@KARVY is not null and @KARVYCODE is null)
        BEGIN
				 Insert into ProductAMCSchemeMapping
				 (
				 PASP_SchemePlanCode,
				 PASC_AMC_ExternalCode,
				 PASC_AMC_ExternalType
				 )
				 Values
				 (
				 @WERPCODE,@KARVY,'KARVY'
				 )
		END
		ELSE IF(@CAMS is null and @CAMSCODE is not null)
	    BEGIN
				  DELETE from ProductAMCSchemeMapping
				  where PASP_SchemePlanCode= @WERPCODE
				  AND PASC_AMC_ExternalCode = @KARVYCODE
				  AND PASC_AMC_ExternalType='KARVY'
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
 