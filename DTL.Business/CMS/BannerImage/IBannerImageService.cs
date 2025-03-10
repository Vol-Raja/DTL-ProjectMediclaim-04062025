using DTL.Model.Models.CMS;
using System;
using System.Collections.Generic;

namespace DTL.Business.CMS.BannerImage
{
    public interface IBannerImageService
    {
        public string AddEditBannerImageData(BannerImageModel BannerImageModel, bool isEdit);
        public IEnumerable<BannerImageModel> GetBannerImageModelByParam(Guid? Id, bool? IsDeleted, bool? IsArchieved, bool? IsGallery);
    }
}
