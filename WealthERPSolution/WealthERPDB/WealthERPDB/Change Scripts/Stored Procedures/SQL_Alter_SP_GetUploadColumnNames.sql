


-- =============================================
-- Author:		<Benson>
-- Create date: <06/05/2009,>
-- Description:	<Used for getting Column Names and MAndatory Flag Check for a selected type of profile file Upload>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetUploadColumnNames]
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



 