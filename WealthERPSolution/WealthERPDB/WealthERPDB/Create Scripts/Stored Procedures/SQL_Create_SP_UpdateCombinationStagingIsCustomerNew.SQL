/****** Object:  StoredProcedure [dbo].[SP_UpdateCombinationStagingIsCustomerNew]    Script Date: 06/24/2009 12:06:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCombinationStagingIsCustomerNew]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateCombinationStagingIsCustomerNew]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCombinationStagingIsCustomerNew]    Script Date: 06/24/2009 12:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCombinationStagingIsCustomerNew]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_UpdateCombinationStagingIsCustomerNew]
AS
   update CustomerMFKarvyXtrnlCombinationStaging 
     set CMFKXCS_IsCustomerNew=0,CMFKXCS_CustomerId = c.C_CustomerId 
     from CustomerMFKarvyXtrnlCombinationStaging s,Customer c 
     where s.CMFKXCS_PANNumber=c.C_PANNum and s.CMFKXCS_IsCustomerNew=1
' 
END
GO
