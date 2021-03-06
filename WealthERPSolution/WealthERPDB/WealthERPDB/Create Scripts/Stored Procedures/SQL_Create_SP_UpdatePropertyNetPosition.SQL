/****** Object:  StoredProcedure [dbo].[SP_UpdatePropertyNetPosition]    Script Date: 06/24/2009 12:07:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdatePropertyNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdatePropertyNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdatePropertyNetPosition]    Script Date: 06/24/2009 12:07:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdatePropertyNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_UpdatePropertyNetPosition]
@CPNP_PropertyNPId INT,
@XMC_MeasureCode VARCHAR(5),
@CPNP_Name VARCHAR(50),
@CPNP_PropertyAdrLine1 VARCHAR(30),
@CPNP_PropertyAdrLine2 VARCHAR(30),
@CPNP_PropertyAdrLine3 VARCHAR(30),
@CPNP_PropertyCity VARCHAR(30),
@CPNP_PropertyState VARCHAR(30),
@CPNP_PropertyCountry VARCHAR(30),
@CPNP_PropertyPinCode NUMERIC(6,0),
@CPNP_PurchaseDate DATETIME,
@CPNP_PurchasePrice NUMERIC(18,3),
@CPNP_Quantity NUMERIC(18,5),
@CPNP_CurrentPrice NUMERIC(18,3),
@CPNP_CurrentValue NUMERIC(18,3),
@CPNP_PurchaseValue NUMERIC(18,3),
@CPNP_SellDate DATETIME,
@CPNP_SellPrice NUMERIC(18,3),
@CPNP_SellValue NUMERIC(18,3),
@CPNP_Remark VARCHAR(100),
@CPNP_ModifiedBy INT

AS

BEGIN
	
	UPDATE 
		CustomerPropertyNetPosition
	SET
		XMC_MeasureCode = @XMC_MeasureCode,
		CPNP_Name = @CPNP_Name,
		CPNP_PropertyAdrLine1 = @CPNP_PropertyAdrLine1,
		CPNP_PropertyAdrLine2 = @CPNP_PropertyAdrLine2,
		CPNP_PropertyAdrLine3 = @CPNP_PropertyAdrLine3,
		CPNP_PropertyCity = @CPNP_PropertyCity,
		CPNP_PropertyState = @CPNP_PropertyState,
		CPNP_PropertyCountry = @CPNP_PropertyCountry,
		CPNP_PropertyPinCode = @CPNP_PropertyPinCode,
		CPNP_PurchaseDate = @CPNP_PurchaseDate,
		CPNP_PurchasePrice = @CPNP_PurchasePrice,
		CPNP_Quantity = @CPNP_Quantity,
		CPNP_CurrentPrice = @CPNP_CurrentPrice,
		CPNP_CurrentValue = @CPNP_CurrentValue,
		CPNP_PurchaseValue = @CPNP_PurchaseValue,
		CPNP_SellDate = @CPNP_SellDate,
		CPNP_SellPrice = @CPNP_SellPrice,
		CPNP_SellValue = @CPNP_SellValue,
		CPNP_Remark = @CPNP_Remark,
		CPNP_ModifiedBy = @CPNP_ModifiedBy
	WHERE
		CPNP_PropertyNPId = @CPNP_PropertyNPId
	
END ' 
END
GO
