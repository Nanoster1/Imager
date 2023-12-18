using System;
using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Contracts.Models;
using Imager.ImageStoreService.Core.Images.Commands.CreateImageFromTemp;
using Imager.ImageStoreService.Core.Images.Queries.GetImage;
using Imager.ImageStoreService.Core.Images.Results;
using Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces;

using ImageFileModel2 = Imager.ImageStoreService.Core.Images.Models.ImageFileModel;

namespace Imager.ImageStoreService.Server.Mapping
{
    public partial class ImageMapper : IImageMapper
    {
        public CreateImageFromTempCommand Map(CreateImageFromTempRequest p1)
        {
            return p1 == null ? null : new CreateImageFromTempCommand(p1.UserId, p1.TempImageId, p1.DeleteTempImage);
        }
        public CreateImageFromTempResponse Map(CreateImageFromTempResult p2)
        {
            return p2 == null ? null : new CreateImageFromTempResponse(p2.UserId, p2.ImageId);
        }
        public GetImageQuery Map(GetImageRequest p3)
        {
            return p3 == null ? null : new GetImageQuery(p3.UserId, p3.ImageId);
        }
        public GetImageResponse Map(GetImageResult p4)
        {
            return p4 == null ? null : new GetImageResponse(p4.ImageId, funcMain1(p4.Image));
        }
        
        private ImageFileModel funcMain1(ImageFileModel2 p5)
        {
            return p5 == null ? null : new ImageFileModel(funcMain2(p5.ImageInBytes), p5.Format);
        }
        
        private byte[] funcMain2(byte[] p6)
        {
            if (p6 == null)
            {
                return null;
            }
            byte[] result = new byte[p6.Length];
            Array.Copy(p6, 0, result, 0, p6.Length);
            return result;
            
        }
    }
}