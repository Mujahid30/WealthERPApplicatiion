/****** Object:  StoredProcedure [dbo].[SP_GetProfileFilterCategory]    Script Date: 06/24/2009 12:04:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProfileFilterCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetProfileFilterCategory]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProfileFilterCategory]    Script Date: 06/24/2009 12:04:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProfileFilterCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetProfileFilterCategory
@WPFC_AssetClass VARCHAR(50),
@XCT_CustomerTypeCode VARCHAR(5),
@WPFC_KYFCompliantFlag TINYINT

AS

SELECT WPFC_FilterCategoryCode FROM dbo.WerpProfileFilterCategory
WHERE
WPFC_AssetClass=@WPFC_AssetClass
AND
XCT_CustomerTypeCode=@XCT_CustomerTypeCode
AND
WPFC_KYFCompliantFlag=@WPFC_KYFCompliantFlag' 
END
GO
