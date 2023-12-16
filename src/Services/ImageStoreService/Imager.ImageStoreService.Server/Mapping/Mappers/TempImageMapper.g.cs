using System;
using System.Collections.Generic;
using Imager.ImageStoreService.Contracts.HttpRequests;
using Imager.ImageStoreService.Contracts.HttpResponses;
using Imager.ImageStoreService.Core.Domains.TempImages.Commands.CreateTempImages;
using Imager.ImageStoreService.Core.Domains.TempImages.Queries.GetTempImage;
using Imager.ImageStoreService.Core.Domains.TempImages.Results;
using Imager.ImageStoreService.Server.Mapping.Mappers.Interfaces;

namespace Imager.ImageStoreService.Server.Mapping
{
    public partial class TempImageMapper : ITempImageMapper
    {
        public CreateTempImagesCommand Map(CreateTempImagesRequest p1)
        {
            return p1 == null ? null : new CreateTempImagesCommand(p1.UserId, funcMain1(p1.Images));
        }
        public CreateTempImageResponse Map(CreateTempImageResult p4)
        {
            return p4 == null ? null : new CreateTempImageResponse(funcMain3(p4.ImagesIds));
        }
        public GetTempImageQuery Map(GetTempImageRequest p6)
        {
            return p6 == null ? null : new GetTempImageQuery(p6.ImageId, p6.UserId);
        }
        public GetTempImageResponse Map(GetTempImageResult p7)
        {
            return p7 == null ? null : new GetTempImageResponse(p7.ImageId, funcMain4(p7.Image));
        }
        
        private List<byte[]> funcMain1(List<byte[]> p2)
        {
            if (p2 == null)
            {
                return null;
            }
            List<byte[]> result = new List<byte[]>(p2.Count);
            
            int i = 0;
            int len = p2.Count;
            
            while (i < len)
            {
                byte[] item = p2[i];
                result.Add(funcMain2(item));
                i++;
            }
            return result;
            
        }
        
        private List<string> funcMain3(List<string> p5)
        {
            if (p5 == null)
            {
                return null;
            }
            List<string> result = new List<string>(p5.Count);
            
            int i = 0;
            int len = p5.Count;
            
            while (i < len)
            {
                string item = p5[i];
                result.Add(item);
                i++;
            }
            return result;
            
        }
        
        private byte[] funcMain4(byte[] p8)
        {
            if (p8 == null)
            {
                return null;
            }
            byte[] result = new byte[p8.Length];
            Array.Copy(p8, 0, result, 0, p8.Length);
            return result;
            
        }
        
        private byte[] funcMain2(byte[] p3)
        {
            if (p3 == null)
            {
                return null;
            }
            byte[] result = new byte[p3.Length];
            Array.Copy(p3, 0, result, 0, p3.Length);
            return result;
            
        }
    }
}