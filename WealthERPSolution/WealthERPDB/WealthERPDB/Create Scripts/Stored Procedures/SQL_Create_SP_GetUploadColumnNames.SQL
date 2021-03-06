/****** Object:  StoredProcedure [dbo].[SP_GetUploadColumnNames]    Script Date: 06/24/2009 12:05:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetUploadColumnNames]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetUploadColumnNames]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUploadColumnNames]    Script Date: 06/24/2009 12:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetUploadColumnNames]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Benson>
-- Create date: <06/05/2009,>
-- Description:	<Used for getting Column Names and MAndatory Flag Check for a selected type of profile file Upload>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetUploadColumnNames]
@ExternalfileSource varchar(5),
@ExternalFiletype varchar(25),
@ExternalFileExtension varchar(20),
@AssetGroup varchar(5)
AS

select 
	WESH.[WESHM_ExternalColumnName] ExternalColumnName, 
	WESH.[WESHM_IsMandatory] IsMandatory,
	WESH.[WESHM_WerpNameOfExternalColumn] WERPColumnName
from 
	WerpExternalSourceHeaderMaster WESH
	inner join [XMLExternalSourceFileType] XESF on 
	XESF.[XESFT_FileTypeId] = WESH.[XESFT_FileTypeId]
where XESF.[XES_SourceCode]=@ExternalfileSource
		and XESF.[XESFT_FileType] = @ExternalFiletype
		and XESF.[XESFT_FileExtension] = @ExternalFileExtension
		and XESF.[XESFT_AssetGroup] = @AssetGroup



' 
END
GO
