using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.UnitOfWork.Collections;
using System;
using System.Globalization;
using System.Reflection.Metadata;
using QuanLyMoiTruong.Application.Requests;
using System.Linq.Expressions;
using QuanLyMoiTruong.Common.Expressions;
using QuanLyMoiTruong.Application.Request;
using QuanLyMoiTruong.Common.Enums;

namespace QuanLyMoiTruong.Application.Services
{

    public class BaoCaoBaoVeMoiTruongKCNService : IBaoCaoBaoVeMoiTruongKCNService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;

        public BaoCaoBaoVeMoiTruongKCNService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService)
        {
            _unitOfWork= unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted= true;
                return new ApiSuccessResult<bool>() {};
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa") ;
            }
        } 

        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongKCNViewModel>>> GetAll()
        {
            var result = new List<BaoCaoBaoVeMoiTruongKCNViewModel>();
            var entities =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongKCNViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongKCNViewModel>> GetById(int id)
        {
            var result = new BaoCaoBaoVeMoiTruongKCNViewModel();
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoBaoVeMoiTruongKCN == id, include: x => x.Include(y => y.KhuCongNghiep));
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdBaoCaoBaoVeMoiTruongKCN, NhomTaiLieuEnum.BaoCaoBaoVeMoiTruongKCN.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongKCNViewModel>() {Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongKCN>> Insert(BaoCaoBaoVeMoiTruongKCNViewModel obj)
        {
            var entity = new BaoCaoBaoVeMoiTruongKCN();
            entity = MapViewModelToEntity(obj);
            await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoBaoVeMoiTruongKCN;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoBaoVeMoiTruongKCN.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongKCN>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<BaoCaoBaoVeMoiTruongKCNViewModel>>> GetAllPaging(BaoCaoBaoVeMoiTruongKCNRequest request)
        {
            Expression<Func<BaoCaoBaoVeMoiTruongKCN, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenBaoCao.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<BaoCaoBaoVeMoiTruongKCNViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<BaoCaoBaoVeMoiTruongKCNViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongKCN>> Update(BaoCaoBaoVeMoiTruongKCNViewModel obj)
        {
            var entity = new BaoCaoBaoVeMoiTruongKCN();
            entity = MapViewModelToEntity(obj);
            _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoBaoVeMoiTruongKCN;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoBaoVeMoiTruongKCN.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongKCN>() { Data = entity };
        }
        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongKCNViewModel>>> GetListBaoCaoBaoVeMoiTruongByKCN(int idKhuCongNghiep)
        {
            var result = new List<BaoCaoBaoVeMoiTruongKCNViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdKhuCongNghiep == idKhuCongNghiep, include: x => x.Include(x => x.KhuCongNghiep));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoBaoVeMoiTruongKCN, NhomTaiLieuEnum.BaoCaoBaoVeMoiTruongKCN.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongKCNViewModel>>() { Data = result };
        }
        public BaoCaoBaoVeMoiTruongKCNViewModel MapEntityToViewModel(BaoCaoBaoVeMoiTruongKCN entity) {
            var result = new BaoCaoBaoVeMoiTruongKCNViewModel();
            result.IdBaoCaoBaoVeMoiTruongKCN = entity.IdBaoCaoBaoVeMoiTruongKCN;
            result.TenBaoCao = entity.TenBaoCao;
            result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.KhuCongNghiep.TenKhuCongNghiep;
            return result;
        }
        public BaoCaoBaoVeMoiTruongKCN MapViewModelToEntity(BaoCaoBaoVeMoiTruongKCNViewModel viewModel)
        {
            var entity = new BaoCaoBaoVeMoiTruongKCN();
            entity.IdBaoCaoBaoVeMoiTruongKCN = viewModel.IdBaoCaoBaoVeMoiTruongKCN;
            entity.TenBaoCao = viewModel.TenBaoCao;
            entity.NgayBaoCao = string.IsNullOrEmpty(viewModel.NgayBaoCao) ? null : DateTime.Parse(viewModel.NgayBaoCao, new CultureInfo("vi-VN"));
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep;
            return entity;
        }
    }

}
