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
using QuanLyMoiTruong.Common.Enums;
using QuanLyMoiTruong.Application.Request;

namespace QuanLyMoiTruong.Application.Services
{

    public class BaoCaoBaoVeMoiTruongHangNamService : IBaoCaoBaoVeMoiTruongHangNamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;

        public BaoCaoBaoVeMoiTruongHangNamService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService)
        {
            _unitOfWork= unitOfWork;
            _fileTaiLieuService= fileTaiLieuService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted= true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() {};
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa") ;
            }
        } 

        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongHangNamViewModel>>> GetAll()
        {
            var result = new List<BaoCaoBaoVeMoiTruongHangNamViewModel>();
            var entities =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongHangNamViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongHangNamViewModel>> GetById(int id)
        {
            var result = new BaoCaoBaoVeMoiTruongHangNamViewModel();
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoBaoVeMoiTruongHangNam == id, include: x => x.Include(y => y.DuAn));
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdBaoCaoBaoVeMoiTruongHangNam, NhomTaiLieuEnum.BaoCaoBaoVeMoiTruongHangNam.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongHangNamViewModel>() {Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongHangNam>> Insert(BaoCaoBaoVeMoiTruongHangNamViewModel obj)
        {
            var entity = new BaoCaoBaoVeMoiTruongHangNam();
            entity = MapViewModelToEntity(obj);
            await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoBaoVeMoiTruongHangNam;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoBaoVeMoiTruongHangNam.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongHangNam>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<BaoCaoBaoVeMoiTruongHangNamViewModel>>> GetAllPaging(BaoCaoBaoVeMoiTruongHangNamRequest request)
        {
            Expression<Func<BaoCaoBaoVeMoiTruongHangNam, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenBaoCao.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<BaoCaoBaoVeMoiTruongHangNamViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<BaoCaoBaoVeMoiTruongHangNamViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongHangNam>> Update(BaoCaoBaoVeMoiTruongHangNamViewModel obj)
        {
            var entity = new BaoCaoBaoVeMoiTruongHangNam();
            entity = MapViewModelToEntity(obj);
            _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoBaoVeMoiTruongHangNam;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoBaoVeMoiTruongHangNam.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongHangNam>() { Data = entity };
        }
        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongHangNamViewModel>>> GetListBaoCaoBaoVeMoiTruongHangNamByDuAn(int idDuAn)
        {
            var result = new List<BaoCaoBaoVeMoiTruongHangNamViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdDuAn == idDuAn, include: x => x.Include(x => x.DuAn));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoBaoVeMoiTruongHangNam, NhomTaiLieuEnum.BaoCaoBaoVeMoiTruongHangNam.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongHangNamViewModel>>() { Data = result };
        }

        public BaoCaoBaoVeMoiTruongHangNamViewModel MapEntityToViewModel(BaoCaoBaoVeMoiTruongHangNam entity) {
            var result = new BaoCaoBaoVeMoiTruongHangNamViewModel();
            result.IdBaoCaoBaoVeMoiTruongHangNam = entity.IdBaoCaoBaoVeMoiTruongHangNam;
            result.TenBaoCao = entity.TenBaoCao;
            result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.DuAn.TenDuAn;
            result.TenDoanhNghiep = entity.DuAn.TenDoanhNghiep;
            return result;
        }
        public BaoCaoBaoVeMoiTruongHangNam MapViewModelToEntity(BaoCaoBaoVeMoiTruongHangNamViewModel viewModel)
        {
            var entity = new BaoCaoBaoVeMoiTruongHangNam();
            entity.IdBaoCaoBaoVeMoiTruongHangNam = viewModel.IdBaoCaoBaoVeMoiTruongHangNam;
            entity.TenBaoCao = viewModel.TenBaoCao;
            entity.NgayBaoCao = string.IsNullOrEmpty(viewModel.NgayBaoCao) ? null : DateTime.Parse(viewModel.NgayBaoCao, new CultureInfo("vi-VN"));
            entity.IdDuAn = viewModel.IdDuAn;

            return entity;
        }
    }

}
