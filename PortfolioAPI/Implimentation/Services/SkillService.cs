using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Ifrastacture.DataBase;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
namespace PortfolioAPI.Implimentation.Skills
{
    public class SkillService:ISkillService
    {
        private readonly ContentDataContext _contentDataContext;
        public SkillService(ContentDataContext contentDataContext) {
            _contentDataContext = contentDataContext;       
        }

        public async Task<ResponseData<Guid>> AddSkill(SkillDto skillDto)
        {
            try
            {
                if (skillDto == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = true,
                        Message = "Successfully Updated",
                    };
                }
                var data = new Ifrastacture.Models.Skills
                {
                    Id = Guid.NewGuid(),
                    LanguageName = skillDto.LanguageName,
                    Percent = skillDto.Percent,
                    IsActive = true,
                    Status = "Not Approved"

                };
                await _contentDataContext.skills.AddAsync(data);
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfully Added",
                    data = data.Id
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<Guid>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    // data = getData.Id
                };
            }
        }

        public async Task<ResponseData<SkillDto>> ApproveSkill(Guid id)
        {

            try
            {
                var getData = await _contentDataContext.skills.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<SkillDto>
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }

                getData.Status = "Approved";
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<SkillDto>
                {
                    IsSuccess = true,
                    Message = "Successfully Approved",
                    // data = getData.Id
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<SkillDto>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    // data = getData.Id
                };
            }

        }

        public async Task<ResponseData> DeleteSkill(Guid id)
        {

            try
            {
                var getData = await _contentDataContext.skills.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }

                getData.IsActive = false;
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData
                {
                    IsSuccess = true,
                    Message = "Successfully Deleted",
                    // data = getData.Id
                };
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    // data = getData.Id
                };
            }

        }

        public async Task<ResponseData<List<SkillDto>>> GetAllApprovedSkills()
        {

            try
            {
                var getData = await _contentDataContext.skills.Where(x => x.Status == "Approved" && x.IsActive == true).Select(x => new SkillDto
                {
                    Id = x.Id,
                    LanguageName = x.LanguageName,
                    Percent = x.Percent,
                    Status = x.Status,
                    IsActive = x.IsActive,

                }).ToListAsync();
                if (getData == null)
                {
                    return new ResponseData<List<SkillDto>>
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }
                return new ResponseData<List<SkillDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched",
                    data = getData
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<List<SkillDto>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    // data = getData.Id
                };
            }

        }

        public async Task<ResponseData<List<SkillDto>>> GetAllSkills()
        {

            try
            {
                var getData = await _contentDataContext.skills.Where(x => x.IsActive == true).Select(x => new SkillDto
                {
                    Id = x.Id,
                    LanguageName = x.LanguageName,
                    Percent = x.Percent,
                    Status = x.Status,
                    IsActive = x.IsActive,

                }).ToListAsync();
                if (getData == null)
                {
                    return new ResponseData<List<SkillDto>>
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }
                return new ResponseData<List<SkillDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched",
                    data = getData
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<List<SkillDto>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    // data = getData.Id
                };
            }

        }

        public async Task<ResponseData<SkillDto>> GetSkillById(Guid id)
        {


            try
            {
                var getData = await _contentDataContext.skills.Where(x => x.Id == id && x.IsActive == true).Select(x => new SkillDto
                {
                    Id = x.Id,
                    LanguageName = x.LanguageName,
                    Percent = x.Percent,
                    Status = x.Status,
                    IsActive= x.IsActive,

                }).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<SkillDto>
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }
                return new ResponseData<SkillDto>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched",
                    data = getData
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<SkillDto>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

        }

        public async Task<ResponseData<Guid>> UpdateSkill(SkillDto SkillDto)
        {


            try
            {
                var getData = await _contentDataContext.skills.Where(x => x.Id == SkillDto.Id && x.IsActive == true).Select(x => new SkillDto
                {
                    Id = x.Id,
                    LanguageName = x.LanguageName,
                    Percent = x.Percent,
                    Status = x.Status,


                }).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }
                getData.LanguageName = SkillDto.LanguageName;
                getData.Percent = SkillDto.Percent;
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfully Updated",
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<Guid>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    // data = getData.Id
                };
            }

        }


    }
}
