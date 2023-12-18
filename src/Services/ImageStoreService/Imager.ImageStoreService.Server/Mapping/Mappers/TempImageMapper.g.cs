using System;
using System.Collections.Generic;
using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Core.TempImages.Commands.CreateTempImages;
using Imager.ImageStoreService.Core.TempImages.Models;
using Imager.ImageStoreService.Core.TempImages.Queries.GetTempImage;
using Imager.ImageStoreService.Core.TempImages.Results;
using Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces;

using TempImageFileModel2 = Imager.ImageStoreService.Contracts.Models.TempImageFileModel;

namespace Imager.ImageStoreService.Server.Mapping
{
    public partial class TempImageMapper : ITempImageMapper
    {
        public CreateTempImagesCommand Map(CreateTempImagesRequest p1)
        {
            return p1 == null ? null : new CreateTempImagesCommand(p1.UserId, funcMain1(p1.Images));
        }
        public CreateTempImagesResponse Map(CreateTempImagesResult p5)
        {
            return p5 == null ? null : new CreateTempImagesResponse(funcMain4(p5.ImagesIds));
        }
        public GetTempImageQuery Map(GetTempImageRequest p7)
        {
            return p7 == null ? null : new GetTempImageQuery(p7.ImageId, p7.UserId);
        }
        public GetTempImageResponse Map(GetTempImageResult p8)
        {
            return p8 == null ? null : new GetTempImageResponse(p8.ImageId, funcMain5(p8.Image));
        }
        
        private List<TempImageFileModel> funcMain1(List<TempImageFileModel2> p2)
        {
            if (p2 == null)
            {
                return null;
            }
            List<TempImageFileModel> result = new List<TempImageFileModel>(p2.Count);
            
            int i = 0;
            int len = p2.Count;
            
            while (i < len)
            {
                TempImageFileModel2 item = p2[i];
                result.Add(funcMain2(item));
                i++;
            }
            return result;
            
        }
        
        private List<string> funcMain4(List<string> p6)
        {
            if (p6 == null)
            {
                return null;
            }
            List<string> result = new List<string>(p6.Count);
            
            int i = 0;
            int len = p6.Count;
            
            while (i < len)
            {
                string item = p6[i];
                result.Add(item);
                i++;
            }
            return result;
            
        }
        
        private TempImageFileModel2 funcMain5(TempImageFileModel p9)
        {
            return p9 == null ? null : new TempImageFileModel2(funcMain6(p9.ImageInBytes), p9.Format);
        }
        
        private TempImageFileModel funcMain2(TempImageFileModel2 p3)
        {
            return p3 == null ? null : new TempImageFileModel(funcMain3(p3.ImageInBytes), p3.Format);
        }
        
        private byte[] funcMain6(byte[] p10)
        {
            if (p10 == null)
            {
                return null;
            }
            byte[] result = new byte[p10.Length];
            Array.Copy(p10, 0, result, 0, p10.Length);
            return result;
            
        }
        
        private byte[] funcMain3(byte[] p4)
        {
            if (p4 == null)
            {
                return null;
            }
            byte[] result = new byte[p4.Length];
            Array.Copy(p4, 0, result, 0, p4.Length);
            return result;
            
        }
    }
}